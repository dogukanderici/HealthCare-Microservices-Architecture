using AutoMapper;
using Core.AdminUI.Commons.Extensions;
using Core.AdminUI.Models.WorkflowEngineModels.ProcessTaskActionModels;
using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessTaskActionDtos;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Microsoft.AspNetCore.Mvc;

namespace Core.AdminUI.Controllers
{
    [Route("WorkflowEngine/TaskAction")]
    public class ProcessTaskActionController : BaseController
    {
        private readonly IProcessTaskActionService _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private string ControllerHomeEndpoint;
        private string HomeEndpoint;

        public ProcessTaskActionController(IProcessTaskActionService service, IMapper mapper, IConfiguration configuration)
        {
            _service = service;
            _mapper = mapper;
            _configuration = configuration;

            ControllerHomeEndpoint = _configuration.GetSection("ApiSetting:WorkflowEngine:UIUrl:ProcessTaskAction").Value;
            HomeEndpoint = _configuration.GetSection("ApiSetting:DefaultUrl").Value;
        }

        [HttpGet("task/{id:guid}")]
        public async Task<IActionResult> Index(Guid id)
        {
            UIApiResponse<IReadOnlyCollection<ProcessTaskActionResultDto>> apiResponse = await _service.GetListAsync(id);

            ProcessTaskActionResultModel<IReadOnlyCollection<ProcessTaskActionResultDto>> model = new ProcessTaskActionResultModel<IReadOnlyCollection<ProcessTaskActionResultDto>>();

            model.ModelData = apiResponse.Data;
            model.ProcessTaskId = id;
            model.PagingToken = apiResponse.PagingToken;

            ViewResult view = View(model);
            RedirectResult redirect = Redirect(HomeEndpoint);

            return apiResponse.ReturnControllerResult(view, redirect);
        }

        [HttpGet("CreatePartial/{processTaskId:guid}")]
        public IActionResult CreatePartial(Guid processTaskId)
        {
            ProcessTaskActionCreateModel model = new ProcessTaskActionCreateModel();
            model.ModelData.ProcessTaskId = processTaskId;

            return PartialView("_CreateTaskActionPartial", model);
        }

        [HttpPost("Create/{processTaskId:guid}")]
        public async Task<IActionResult> Create(ProcessTaskActionCreateModel model)
        {
            UIApiResponse<Guid> apiResponse = await _service.CreateAsync(model.ModelData);

            RedirectResult redirect = Redirect($"{ControllerHomeEndpoint}/task/{model.ModelData.ProcessTaskId}");

            return apiResponse.ReturnControllerResult(redirect, TempData);
        }

        [HttpGet("UpdatePartial/{id:guid}")]
        public async Task<IActionResult> UpdatePartial(Guid id)
        {
            UIApiResponse<ProcessTaskActionResultDto> apiResponse = await _service.GetAsync(id);

            ProcessTaskActionUpdateModel model = new ProcessTaskActionUpdateModel();

            model.ModelData = _mapper.Map<ProcessTaskActionUpdateDto>(apiResponse.Data);

            return PartialView("_UpdateTaskActionPartial", model);
        }

        [HttpPost("Update/{id:guid}")]
        public async Task<IActionResult> Update(ProcessTaskActionUpdateModel model)
        {
            UIApiResponse<DateTimeOffset> apiResponse = await _service.UpdateAsync(model.ModelData);

            RedirectResult redirect = Redirect($"{ControllerHomeEndpoint}/task/{model.ModelData.ProcessTaskId}");

            return apiResponse.ReturnControllerResult(redirect, TempData);
        }

        [HttpGet("DeletePartial/{processTaskId:guid}")]
        public IActionResult DeletePartial(Guid processTaskId, Guid id)
        {
            ViewBag.Id = id;
            ViewBag.ProcessTaskId = processTaskId;

            return PartialView("_DeleteTaskActionPartial");
        }

        [HttpPost("Delete/{processTaskId:guid}")]
        public async Task<IActionResult> Delete(Guid processTaskId, Guid id)
        {
            UIApiResponse<bool> apiResponse = await _service.DeleteAsync(id);

            RedirectResult redirect = Redirect($"{ControllerHomeEndpoint}/task/{processTaskId}");

            return apiResponse.ReturnControllerResult(redirect, TempData);
        }
    }
}
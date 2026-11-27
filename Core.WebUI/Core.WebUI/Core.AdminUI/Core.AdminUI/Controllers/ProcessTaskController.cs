using AutoMapper;
using Core.AdminUI.Commons.Extensions;
using Core.AdminUI.Models.WorkflowEngineModels.ProcessTaskModels;
using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessTaskDtos;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Core.AdminUI.Controllers
{
    [Route("WorkflowEngine/ProcessTask")]
    public class ProcessTaskController : BaseController
    {
        private readonly IProcessTaskService _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ProcessTaskController(IProcessTaskService service, IMapper mapper, IConfiguration configuration)
        {
            _service = service;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet("{processId:guid}")]
        public async Task<IActionResult> Index(Guid processId)
        {
            UIApiResponse<IReadOnlyCollection<ProcessTaskResultDto>> serviceResponse = await _service.GetListAsync(processId);

            ProcessTaskResultModel<IReadOnlyCollection<ProcessTaskResultDto>> model = new ProcessTaskResultModel<IReadOnlyCollection<ProcessTaskResultDto>>();

            model.ModelData = serviceResponse.Data;
            model.ProcessId = processId;
            model.IsLastPage = serviceResponse.IsLastPage;
            model.PagingToken = serviceResponse.PagingToken;

            ViewResult view = View(model);
            RedirectResult redirect = Redirect(_configuration.GetSection("ApiSetting:DefaultUrl").Value);

            return serviceResponse.ReturnControllerResult(view, redirect);
        }

        [HttpGet("CreatePartial/{processId:guid}")]
        public IActionResult CreatePartial(Guid processId)
        {
            ProcessTaskCreateModel model = new ProcessTaskCreateModel();
            model.ModelData.ProcessId = processId;

            return PartialView("_CreateProcessTaskPartial", model);
        }

        [HttpPost("Create/{processId:guid}")]
        public async Task<IActionResult> Create(ProcessTaskCreateModel model)
        {
            UIApiResponse<Guid> apiServiceResponse = await _service.CreateAsync(model.ModelData);

            RedirectResult redirect = Redirect($"{_configuration.GetSection("ApiSetting:WorkflowEngine:UIUrl:ProcessTask").Value}/{model.ModelData.ProcessId}");

            return apiServiceResponse.ReturnControllerResult(redirect, TempData);
        }

        [HttpGet("UpdatePartial/{id:guid}")]
        public async Task<IActionResult> UpdatePartial(Guid id)
        {
            UIApiResponse<ProcessTaskResultDto> serviceResponse = await _service.GetAsync(id);

            ProcessTaskUpdateModel model = new ProcessTaskUpdateModel();

            model.ModelData = _mapper.Map<ProcessTaskUpdateDto>(serviceResponse.Data);

            if (serviceResponse.Status)
            {
                return PartialView("_UpdateProcessTaskPartial", model);
            }

            return Redirect($"{_configuration.GetSection("ApiSetting:WorkflowEngine:UIUrl:ProcessTask").Value}/{model.ModelData.ProcessId}");
        }

        [HttpPost("Update/{id:guid}")]
        public async Task<IActionResult> Update(ProcessTaskUpdateModel model)
        {
            UIApiResponse<DateTimeOffset> apiServiceResponse = await _service.UpdateAsync(model.ModelData);

            RedirectResult redirect = Redirect($"{_configuration.GetSection("ApiSetting:WorkflowEngine:UIUrl:ProcessTask").Value}/{model.ModelData.ProcessId}");

            return apiServiceResponse.ReturnControllerResult(redirect, TempData);
        }

        [HttpGet("DeletePartial/{processId:guid}/{id:guid}")]
        public IActionResult DeletePartial(Guid processId, Guid id)
        {
            ViewBag.Id = id;
            ViewBag.ProcessId = processId;

            return PartialView("_DeleteProcessTaskPartial");
        }

        [HttpPost("Delete/{processId:guid}")]
        public async Task<IActionResult> Delete(Guid processId, Guid id)
        {
            UIApiResponse<bool> apiServiceResponse = await _service.DeleteAsync(id);

            RedirectResult redirect = Redirect($"{_configuration.GetSection("ApiSetting:WorkflowEngine:UIUrl:ProcessTask").Value}/{processId}");

            return apiServiceResponse.ReturnControllerResult(redirect, TempData);
        }
    }
}
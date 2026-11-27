using AutoMapper;
using Core.AdminUI.Commons.Extensions;
using Core.AdminUI.Models.WorkflowEngineModels.ProcessDefinitionModels;
using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessDefinitionDtos;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Microsoft.AspNetCore.Mvc;

namespace Core.AdminUI.Controllers
{
    [Route("WorkflowEngine/Process")]
    public class ProcessController : BaseController
    {
        private readonly IProcessDefinitionService _service;
        private readonly IMapper _mapper;

        public ProcessController(IProcessDefinitionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            UIApiResponse<IReadOnlyCollection<ProcessDefinitionResultDto>> result = await _service.GetListAsync();

            ProcessDefinitionResultModel<IReadOnlyCollection<ProcessDefinitionResultDto>> model = new ProcessDefinitionResultModel<IReadOnlyCollection<ProcessDefinitionResultDto>>();

            model.ModelData = result.Data;
            model.PagingToken = result.PagingToken;
            model.IsLastPage = result.IsLastPage;

            return View(model);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            ProcessDefinitionCreateModel model = new ProcessDefinitionCreateModel();

            return PartialView("_CreateProcessPartial", model);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ProcessDefinitionCreateModel model)
        {
            UIApiResponse<Guid> serviceResponse = await _service.CreateAsync(model.ModelData);

            if (serviceResponse.Status)
            {
                return Redirect("Index");
            }

            return PartialView("_CreateProcessPartial", model);
        }

        [HttpGet("UpdatePartial/{id:guid}")]
        public async Task<IActionResult> UpdatePartial(Guid id)
        {
            UIApiResponse<ProcessDefinitionResultDto> serviceResponse = await _service.GetAsync(id);

            if (serviceResponse.Status)
            {
                ProcessDefinitionUpdateModel model = new ProcessDefinitionUpdateModel();

                model.ModelData = _mapper.Map<ProcessDefinitionUpdateDto>(serviceResponse.Data);

                return PartialView("_UpdateProcessPartial", model);
            }

            TempData.SetNotification(serviceResponse, UINotificaitonEnums.NotificationType.Error, "Bir hata oluştu!");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("Update/{id:guid}")]
        public async Task<IActionResult> Update(ProcessDefinitionUpdateModel model)
        {
            UIApiResponse<DateTimeOffset> serviceResponse = await _service.UpdateAsync(model.ModelData);

            if (serviceResponse.Status)
            {
                TempData.SetNotification(serviceResponse, UINotificaitonEnums.NotificationType.Error, "Bir hata oluştu!");
            }

            TempData.SetNotification(serviceResponse, UINotificaitonEnums.NotificationType.Success, "İşlem Başarılı!");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("DeletePartial/{id:guid}")]
        public IActionResult DeletePartial(Guid id)
        {
            return PartialView("_DeleteProcessPartial", id);
        }

        [HttpPost("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            UIApiResponse<bool> serviceResponse = await _service.DeleteAsync(id);

            if (serviceResponse.Status)
            {
                TempData.SetNotification(serviceResponse, UINotificaitonEnums.NotificationType.Error, "Bir hata oluştu!");
            }

            TempData.SetNotification(serviceResponse, UINotificaitonEnums.NotificationType.Success, "İşlem Başarılı!");

            return RedirectToAction(nameof(Index));
        }
    }
}
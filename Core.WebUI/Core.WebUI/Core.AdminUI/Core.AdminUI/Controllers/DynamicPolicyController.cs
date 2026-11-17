using AutoMapper;
using Core.AdminUI.Commons.Extensions;
using Core.AdminUI.Models.WorkflowEngineModels.DynamicPolicyModels;
using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicPolicyDtos;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Microsoft.AspNetCore.Mvc;
using static Core.WebUI.Application.Commons.Parameters.UINotificaitonEnums;

namespace Core.AdminUI.Controllers
{
    [Route("DynamicPolicy")]
    public class DynamicPolicyController : Controller
    {
        private readonly IDynamicPolicyService _service;
        private readonly IMapper _mapper;

        public DynamicPolicyController(IDynamicPolicyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            UIApiResponse<IReadOnlyCollection<DynamicPolicyResultDto>> serviceResponse = await _service.GetListAsync();

            DynamicPolicyResultModel<IReadOnlyCollection<DynamicPolicyResultDto>> model = new DynamicPolicyResultModel<IReadOnlyCollection<DynamicPolicyResultDto>>();

            model.Status = serviceResponse.Status;
            model.ModelData = serviceResponse.Data;

            return View(model);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            DynamicPolicyCreateModel model = new DynamicPolicyCreateModel();

            return View(model);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(DynamicPolicyCreateModel model)
        {
            UIApiResponse<bool> serviceResponse = await _service.CreateAsync(model.CreateDto);

            if (serviceResponse.Status)
            {
                TempData.SetNotification(serviceResponse, NotificationType.Success, "İşlem Başarılı!");
                return Redirect("/dynamicpolicy");
            }

            TempData.SetNotification(serviceResponse, NotificationType.Error, "Bir Hata Oluştu!");

            return View(model.CreateDto);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> Update(Guid id)
        {
            UIApiResponse<DynamicPolicyResultDto> serviceResponse = await _service.GetAsync(id);

            if (serviceResponse.Status)
            {
                DynamicPolicyUpdateModel model = new DynamicPolicyUpdateModel();

                model.ModelData = _mapper.Map<DynamicPolicyUpdateDto>(serviceResponse.Data);

                return View(model);
            }

            TempData.SetNotification(serviceResponse, NotificationType.Error, "Bir Hata Oluştu!");

            return Redirect("/dynamicpolicy");
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(DynamicPolicyUpdateModel model)
        {
            UIApiResponse<bool> serviceResponse = await _service.UpdateAsync(model.ModelData);

            if (serviceResponse.Status)
            {
                TempData.SetNotification(serviceResponse, NotificationType.Success, "İşlem Başarılı!");

                return Redirect("/dynamicpolicy");
            }

            TempData.SetNotification(serviceResponse, NotificationType.Error, "Bir Hata Oluştu!");

            return View(model);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            UIApiResponse<bool> serviceResponse = await _service.DeleteAsync(id);

            if (serviceResponse.Status)
            {
                TempData.SetNotification(serviceResponse, NotificationType.Success, "İşlem Başarılı!");

                return Redirect("/dynamicpolicy");
            }

            TempData.SetNotification(serviceResponse, NotificationType.Error, "Bir Hata Oluştu!");

            return Redirect("/dynamicpolicy");
        }
    }
}
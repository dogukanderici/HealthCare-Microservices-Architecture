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

            ViewResult view = View(model); // İsteğin başarılı olması durumu
            RedirectResult redirect = Redirect("/home/index"); // İsteğin başarız olması durumu

            return serviceResponse.ReturnControllerResult(view, redirect);
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
            UIApiResponse<Guid> serviceResponse = await _service.CreateAsync(model.CreateDto);

            RedirectResult redirect = Redirect("/dynamicpolicy"); // İsteğin başarılı olması durumu
            ViewResult view = View(model); // İsteğin başarız olması durumu

            return serviceResponse.ReturnControllerResult(redirect, view, TempData);

            //if (serviceResponse.Status)
            //{
            //    TempData.SetNotification(serviceResponse, NotificationType.Success, "İşlem Başarılı!");
            //    return Redirect("/dynamicpolicy");
            //}

            //TempData.SetNotification(serviceResponse, NotificationType.Error, "Bir Hata Oluştu!");

            //return View(model.CreateDto);
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
            UIApiResponse<DateTimeOffset> serviceResponse = await _service.UpdateAsync(model.ModelData);

            RedirectResult redirect = Redirect("/dynamicpolicy"); // İsteğin başarılı olması durumu
            ViewResult view = View(model); // İsteğin başarız olması durumu

            return serviceResponse.ReturnControllerResult(redirect, view, TempData);

            //if (serviceResponse.Status)
            //{
            //    TempData.SetNotification(serviceResponse, NotificationType.Success, "İşlem Başarılı!");

            //    return Redirect("/dynamicpolicy");
            //}

            //TempData.SetNotification(serviceResponse, NotificationType.Error, "Bir Hata Oluştu!");

            //return View(model);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            UIApiResponse<bool> serviceResponse = await _service.DeleteAsync(id);

            RedirectResult redirect = Redirect("/dynamicpolicy"); // İsteğin başarılı olması durumu

            return serviceResponse.ReturnControllerResult(redirect, TempData);

            //if (serviceResponse.Status)
            //{
            //    TempData.SetNotification(serviceResponse, NotificationType.Success, "İşlem Başarılı!");

            //    return Redirect("/dynamicpolicy");
            //}

            //TempData.SetNotification(serviceResponse, NotificationType.Error, "Bir Hata Oluştu!");

            //return Redirect("/dynamicpolicy");
        }
    }
}

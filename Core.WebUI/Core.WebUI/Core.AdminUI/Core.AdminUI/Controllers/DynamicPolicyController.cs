using AutoMapper;
using Core.AdminUI.Commons.Extensions;
using Core.AdminUI.Models.WorkflowEngineModels.DynamicPolicyModels;
using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicPolicyDtos;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static Core.WebUI.Application.Commons.Parameters.UINotificaitonEnums;

namespace Core.AdminUI.Controllers
{
    [Route("WorkflowEngine/DynamicPolicy")]
    public class DynamicPolicyController : Controller
    {
        private readonly IDynamicPolicyService _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        private string DynamicPolicyEndpoint;

        public DynamicPolicyController(IDynamicPolicyService service, IMapper mapper, IConfiguration configuration)
        {
            _service = service;
            _mapper = mapper;
            _configuration = configuration;

            DynamicPolicyEndpoint = _configuration.GetSection("ApiSetting:WorkflowEngine:UIUrl:DynamicPolicy").Value;
        }

        [HttpGet("")]
        [HttpPost("")]
        public async Task<IActionResult> Index(DynamicPolicyResultModel<IReadOnlyCollection<DynamicPolicyResultDto>>? resultModel)
        {

            TempData["DynamicPolicyEndpoint"] = DynamicPolicyEndpoint;

            // Gelen istek post ise filtreleme ile arama yapılıyor demektir.
            if (HttpMethods.IsPost(Request.Method))
            {
                if (resultModel?.QueryFilter != null)
                {
                    TempData["PolicyQueryFilter"] = JsonSerializer.Serialize(resultModel.QueryFilter);
                }
                else
                {
                    TempData.Remove("PolicyQueryFilter");
                }

                // View döndürmek yerine tarayıcı GET isteğine yönlendirilir.
                // F5 veya Geri Butonu tıklanmasında ERR_CACHE_MISS hatası alınmaması için.
                return RedirectToAction(nameof(Index));
            }

            if (TempData["PolicyQueryFilter"] is string filterJson && !string.IsNullOrEmpty(filterJson))
            {
                resultModel.QueryFilter = JsonSerializer.Deserialize<DynamicPolicyQueryDto>(filterJson);
            }

            UIApiResponse<IReadOnlyCollection<DynamicPolicyResultDto>> serviceResponse =
                resultModel?.QueryFilter == null ? await _service.GetListAsync() : await _service.GetListWithFilterAsync(resultModel.QueryFilter);

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
            TempData["DynamicPolicyEndpoint"] = DynamicPolicyEndpoint;

            DynamicPolicyCreateModel model = new DynamicPolicyCreateModel();

            return View(model);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(DynamicPolicyCreateModel model)
        {
            UIApiResponse<Guid> serviceResponse = await _service.CreateAsync(model.CreateDto);

            RedirectResult redirect = Redirect($"{DynamicPolicyEndpoint}"); // İsteğin başarılı olması durumu
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
            TempData["DynamicPolicyEndpoint"] = DynamicPolicyEndpoint;

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

            RedirectResult redirect = Redirect($"{DynamicPolicyEndpoint}"); // İsteğin başarılı olması durumu
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

        [HttpGet("DeletePartial/{id:guid}")]
        public async Task<IActionResult> DeletePartial(Guid id)
        {
            ViewBag.Id = id;

            return PartialView("_DeletePolicyPartial", id);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            UIApiResponse<bool> serviceResponse = await _service.DeleteAsync(id);

            RedirectResult redirect = Redirect($"{DynamicPolicyEndpoint}"); // İsteğin başarılı olması durumu

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

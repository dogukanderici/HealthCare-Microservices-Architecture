using Core.AdminUI.Models.WorkflowEngineModels.DynamicPolicyModels;
using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicPolicyDtos;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Microsoft.AspNetCore.Mvc;

namespace Core.AdminUI.Controllers
{
    [Route("DynamicPolicy")]
    public class DynamicPolicyController : Controller
    {
        private readonly IDynamicPolicyService _service;

        public DynamicPolicyController(IDynamicPolicyService service)
        {
            _service = service;
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
                return Redirect("/dynamicpolicy");

            return View(model.CreateDto);
        }
    }
}
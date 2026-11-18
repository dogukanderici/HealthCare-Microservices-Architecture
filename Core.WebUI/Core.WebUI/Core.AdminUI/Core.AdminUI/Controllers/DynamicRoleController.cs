using AutoMapper;
using Core.AdminUI.Commons.Extensions;
using Core.AdminUI.Models.WorkflowEngineModels.DynamicRoleModels;
using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicRoleDtos;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Microsoft.AspNetCore.Mvc;

namespace Core.AdminUI.Controllers
{
    [Route("DynamicRole")]
    public class DynamicRoleController : BaseController
    {
        private readonly IDynamicRoleService _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public DynamicRoleController(IDynamicRoleService service, IMapper mapper, IConfiguration configuration)
        {
            _service = service;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet("{policyId:guid}")]
        public async Task<IActionResult> Index(Guid policyId)
        {
            DynamicRoleQueryDto filter = new DynamicRoleQueryDto();
            filter.dynamicPolicyId = policyId;

            UIApiResponse<IReadOnlyCollection<DynamicRoleResultDto>> serviceResponse = await _service.GetListAsync(policyId);

            DynamicRoleResultModel<IReadOnlyCollection<DynamicRoleResultDto>> model = new DynamicRoleResultModel<IReadOnlyCollection<DynamicRoleResultDto>>();
            model.ModelData = serviceResponse.Data;
            model.PolicyId = policyId;

            ViewResult view = View(model);
            RedirectResult redirect = Redirect(_configuration.GetSection("ApiSetting:DefaultUrl").Value);

            return serviceResponse.ReturnControllerResult(view, redirect);

        }

        [HttpGet("{policyId}/{id}")]
        public async Task<IActionResult> GetRole(Guid policyId, Guid id)
        {
            UIApiResponse<DynamicRoleResultDto> serviceResponse = await _service.GetAsync(id);

            DynamicRoleResultModel<DynamicRoleResultDto> model = new DynamicRoleResultModel<DynamicRoleResultDto>();
            model.ModelData = serviceResponse.Data;

            ViewResult view = View(model);
            RedirectResult redirect = Redirect($"{_configuration.GetSection("ApiSetting:WorkflowEngine:UIUrl:DynamicRole").Value}/{policyId}");

            return serviceResponse.ReturnControllerResult(view, redirect);
        }

        [HttpGet("Create/{policyId}")]
        public IActionResult Create(Guid policyId)
        {
            DynamicRoleCreateModel model = new DynamicRoleCreateModel();
            model.ModelData.DynamicPolicyId = policyId;

            return View(model);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(DynamicRoleCreateModel model)
        {
            UIApiResponse<bool> serviceResponse = await _service.CreateAsync(model.ModelData);

            RedirectResult redirect = Redirect($"{_configuration.GetSection("ApiSetting:WorkflowEngine:UIUrl:DynamicRole").Value}/{model.ModelData.DynamicPolicyId}");
            ViewResult view = View(model);

            return serviceResponse.ReturnControllerResult(redirect, view, TempData);
        }

        [HttpGet("Update/{policyId}/{id}")]
        public async Task<IActionResult> Update(Guid policyId, Guid id)
        {
            UIApiResponse<DynamicRoleResultDto> serviceResponse = await _service.GetAsync(id);

            DynamicRoleUpdateDto updateDto = _mapper.Map<DynamicRoleUpdateDto>(serviceResponse.Data);

            DynamicRoleUpdateModel model = new DynamicRoleUpdateModel();
            model.ModelData = updateDto;

            ViewResult view = View(model);
            RedirectResult redirect = Redirect($"{_configuration.GetSection("ApiSetting:WorkflowEngine:UIUrl:DynamicRole").Value}/{model.ModelData.DynamicPolicyId}");

            return serviceResponse.ReturnControllerResult(view, redirect);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(DynamicRoleUpdateModel model)
        {
            UIApiResponse<bool> serviceResponse = await _service.UpdateAsync(model.ModelData);

            RedirectResult redirect = Redirect($"{_configuration.GetSection("ApiSetting:WorkflowEngine:UIUrl:DynamicRole").Value}/{model.ModelData.DynamicPolicyId}");
            ViewResult view = View(model);

            return serviceResponse.ReturnControllerResult(redirect, view, TempData);
        }

        [HttpPost("Delete/{policyId}")]
        public async Task<IActionResult> Delete(Guid policyId, Guid id)
        {
            UIApiResponse<bool> serviceResponse = await _service.DeleteAsync(id);

            RedirectResult redirect = Redirect($"{_configuration.GetSection("ApiSetting:WorkflowEngine:UIUrl:DynamicRole").Value}/{policyId}");

            return serviceResponse.ReturnControllerResult(redirect, TempData);
        }
    }
}
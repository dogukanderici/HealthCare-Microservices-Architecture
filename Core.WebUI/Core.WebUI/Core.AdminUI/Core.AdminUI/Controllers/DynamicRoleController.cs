using AutoMapper;
using Core.AdminUI.Commons.Extensions;
using Core.AdminUI.Models.WorkflowEngineModels.DynamicRoleModels;
using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.IdentityDtos.RoleDtos;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicRoleDtos;
using Core.WebUI.Application.Interfaces.Services.IdentityServer.RoleService;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.AdminUI.Controllers
{
    [Route("DynamicRole")]
    public class DynamicRoleController : BaseController
    {
        private readonly IDynamicRoleService _service;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public DynamicRoleController(IDynamicRoleService service, IMapper mapper, IConfiguration configuration, IRoleService roleService)
        {
            _service = service;
            _mapper = mapper;
            _configuration = configuration;
            _roleService = roleService;
        }

        [HttpGet("{policyId:guid}")]
        public async Task<IActionResult> Index(Guid policyId)
        {
            DynamicRoleQueryDto filter = new DynamicRoleQueryDto();
            filter.dynamicPolicyId = policyId;

            UIApiResponse<IReadOnlyCollection<DynamicRoleResultDto>> serviceResponse = await _service.GetListWithFilterAsync(filter);

            UIApiResponse<IReadOnlyCollection<RoleResultDto>> roleServiceResponse = await _roleService.GetListAsync();

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

        [HttpGet("Create/{policyId:guid}")]
        public async Task<IActionResult> Create(Guid policyId)
        {
            DynamicRoleCreateModel model = new DynamicRoleCreateModel();
            model.ModelData.DynamicPolicyId = policyId;

            UIApiResponse<IReadOnlyCollection<RoleResultDto>> roleServiceResponse = await _roleService.GetListAsync();

            if (roleServiceResponse.Status)
            {
                if (roleServiceResponse.Data.Any())
                {
                    model.Roles = (from item in roleServiceResponse.Data
                                   select new SelectListItem
                                   {
                                       Text = item.Name,
                                       Value = item.Id.ToString()
                                   }).ToList();
                }
            }

            return View(model);
        }

        [HttpPost("Create/{policyId:guid}")]
        public async Task<IActionResult> Create(DynamicRoleCreateModel model)
        {
            UIApiResponse<Guid> serviceResponse = await _service.CreateAsync(model.ModelData);

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
            UIApiResponse<DateTimeOffset> serviceResponse = await _service.UpdateAsync(model.ModelData);

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
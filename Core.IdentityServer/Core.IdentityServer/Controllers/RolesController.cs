using Core.IdentityServer.Constants;
using Core.IdentityServer.Dtos.RoleDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RolesController> _logger;

        public RolesController(RoleManager<IdentityRole> roleManager, ILogger<RolesController> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "IdentityServerAccessToken", Roles = "Admin")]
        public async Task<IActionResult> AddNewRole(CreateRoleDto createRoleDto)
        {
            if (!await _roleManager.RoleExistsAsync(createRoleDto.RoleName))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(createRoleDto.RoleName));

                if (result.Succeeded)
                {
                    _logger.LogInformation(LogConstant.LogMessageTemplate,
                        LogConstant.ServiceName,
                        nameof(RolesController),
                        nameof(AddNewRole),
                        LogConstant.SuccessMessages.RoleCreatedSuccessfully);

                    return StatusCode(200, LogConstant.SuccessMessages.RoleCreatedSuccessfully);
                }

                _logger.LogError(LogConstant.LogMessageTemplate,
                    LogConstant.ServiceName,
                    nameof(RolesController),
                    nameof(AddNewRole),
                    LogConstant.ErrorMessages.AddNewRoleFailed);

                return StatusCode(400, LogConstant.ErrorMessages.AddNewRoleFailed);
            }

            _logger.LogError(LogConstant.LogMessageTemplate,
                    LogConstant.ServiceName,
                    nameof(RolesController),
                    nameof(AddNewRole),
                    LogConstant.ErrorMessages.RoleAlreadyExists);

            return StatusCode(400, LogConstant.ErrorMessages.RoleAlreadyExists);
        }
    }
}

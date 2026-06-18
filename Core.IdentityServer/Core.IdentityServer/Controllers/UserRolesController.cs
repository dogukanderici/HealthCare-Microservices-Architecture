using Core.IdentityServer.Constants;
using Core.IdentityServer.Dtos.UserRoleDtos;
using Core.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserRolesController> _logger;

        public UserRolesController(UserManager<ApplicationUser> userManager, ILogger<UserRolesController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet("{username}")]
        [Authorize(AuthenticationSchemes = "IdentityServerAccessToken", Roles = "Admin")]
        public async Task<IActionResult> GetUserRoles(string username)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByNameAsync(username);

                if (user == null)
                {
                    _logger.LogError(LogConstant.LogMessageTemplate,
                        LogConstant.ServiceName,
                        nameof(UserRolesController),
                        nameof(GetUserRoles),
                        LogConstant.ErrorMessages.UserNotFound);

                    return StatusCode(400, LogConstant.ErrorMessages.UserNotFound);
                }

                IList<string> userRoles = await _userManager.GetRolesAsync(user);

                _logger.LogInformation(LogConstant.LogMessageTemplate,
                    LogConstant.ServiceName,
                    nameof(UserRolesController),
                    nameof(GetUserRoles),
                    LogConstant.SuccessMessages.ProcessSuccessed);

                return StatusCode(200, userRoles);
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstant.LogMessageTemplate,
                    LogConstant.ServiceName,
                    nameof(UserRolesController),
                    nameof(GetUserRoles),
                    ex.Message);

                return StatusCode(500, LogConstant.ErrorMessages.ProcessFailed);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "IdentityServerAccessToken", Roles = "Admin")]
        public async Task<IActionResult> AddNewRoleToUser(CreateUserRoleDto createUserRoleDto)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByNameAsync(createUserRoleDto.Username);

                if (user == null)
                {
                    _logger.LogError(LogConstant.LogMessageTemplate,
                        LogConstant.ServiceName,
                        nameof(UserRolesController),
                        nameof(GetUserRoles),
                        LogConstant.ErrorMessages.UserNotFound);

                    return StatusCode(400, LogConstant.ErrorMessages.UserNotFound);
                }

                bool userRoleCheck = await _userManager.IsInRoleAsync(user, createUserRoleDto.RoleName);

                if (userRoleCheck)
                {
                    _logger.LogError(LogConstant.LogMessageTemplate,
                        LogConstant.ServiceName,
                        nameof(UserRolesController),
                        nameof(GetUserRoles),
                        LogConstant.ErrorMessages.UserRoleAlreadyExists);

                    return StatusCode(400, LogConstant.ErrorMessages.UserRoleAlreadyExists);
                }

                IdentityResult result = await _userManager.AddToRoleAsync(user, createUserRoleDto.RoleName);

                if (result.Succeeded)
                {
                    _logger.LogInformation(LogConstant.LogMessageTemplate,
                        LogConstant.ServiceName,
                        nameof(UserRolesController),
                        nameof(GetUserRoles),
                        LogConstant.SuccessMessages.UserRoleAssignedSuccessfully);

                    return StatusCode(200, LogConstant.SuccessMessages.UserRoleAssignedSuccessfully);
                }

                _logger.LogError(LogConstant.LogMessageTemplate,
                    LogConstant.ServiceName,
                    nameof(UserRolesController),
                    nameof(GetUserRoles),
                    LogConstant.ErrorMessages.ProcessFailed);

                return StatusCode(400, LogConstant.ErrorMessages.ProcessFailed);
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstant.LogMessageTemplate,
                    LogConstant.ServiceName,
                    nameof(UserRolesController),
                    nameof(GetUserRoles),
                    ex.Message);

                return StatusCode(500, LogConstant.ErrorMessages.ProcessFailed);
            }
        }
    }
}
using Core.IdentityServer.Constants;
using Core.IdentityServer.Dtos;
using Core.IdentityServer.Models;
using Core.IdentityServer.Services.RabbitMQ.Events;
using Core.IdentityServer.Services.RabbitMQ.MessageBus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegistersController> _logger;

        public RegistersController(UserManager<ApplicationUser> userManager, ILogger<RegistersController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "IdentityServerAccessToken", Roles = "Admin")]
        public async Task<IActionResult> UserRegister(RegisterDto registerDto)
        {
            ApplicationUser user = new ApplicationUser
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };

            ApplicationUser userCheck = await _userManager.FindByEmailAsync(user.Email);

            if (userCheck != null)
            {
                string errorMessage = LogConstant.ErrorMessages.UserAlreadyExists;

                _logger.LogError(LogConstant.LogMessageTemplate,
                    LogConstant.ServiceName,
                    nameof(RegistersController),
                    nameof(UserRegister),
                    errorMessage);

                return StatusCode(400, errorMessage);
            }

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                var userRoleAssignResult = await _userManager.AddToRoleAsync(user, "Read");

                if (!userRoleAssignResult.Succeeded)
                {
                    foreach (var error in userRoleAssignResult.Errors)
                    {
                        _logger.LogError(LogConstant.LogMessageTemplate,
                            LogConstant.ServiceName,
                            nameof(RegistersController),
                            nameof(UserRegister),
                            $"{error.Code} - {error.Description}");
                    }

                    return StatusCode(400, userRoleAssignResult.Errors);
                }

                var publisher = new UserModifiedEventPublisher();
                var modifiedUser = new UserModifiedEvent
                {
                    Id = Guid.TryParse(user.Id, out Guid userId) ? userId : Guid.Empty,
                    Name = user.Name,
                    Surname = user.Surname,
                    Username = user.UserName,
                    Email = user.Email
                };

                bool publisherStatus = await publisher.PublisherAsync(modifiedUser);

                _logger.LogInformation(LogConstant.LogMessageTemplate,
                    LogConstant.ServiceName,
                    nameof(RegistersController),
                    nameof(UserRegister),
                    LogConstant.SuccessMessages.UserCreatedSuccessfully);

                return StatusCode(200, LogConstant.SuccessMessages.UserCreatedSuccessfully);
            }

            return StatusCode(400, result.Errors.Select(e => e.Description).ToArray());
        }
    }
}

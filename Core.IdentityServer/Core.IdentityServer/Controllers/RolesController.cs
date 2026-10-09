using AutoMapper;
using Core.IdentityServer.Commons.Constants;
using Core.IdentityServer.Dtos.RoleDtos;
using Core.IdentityServer.Services.RabbitMQ.Events.Roles;
using Core.IdentityServer.Services.RabbitMQ.MessageBuses.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController
    {
        private readonly RoleCreateEventPublisher _publisher;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ILogger<RolesController> _logger;

        public RolesController(RoleCreateEventPublisher publisher, RoleManager<IdentityRole> roleManager, IMapper mapper, ILogger<RolesController> logger)
        {
            _publisher = publisher;
            _roleManager = roleManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "IdentityServerAccessToken", Roles = "Admin")]
        public async Task<IActionResult> AddNewRole(CreateRoleDto createRoleDto)
        {
            if (!await _roleManager.RoleExistsAsync(createRoleDto.RoleName))
            {
                IdentityRole role = new IdentityRole(createRoleDto.RoleName);

                IdentityResult result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    bool publiherResponse = await _publisher.PublishEventAsync(_mapper.Map<RoleCreatedEvent>(role));

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

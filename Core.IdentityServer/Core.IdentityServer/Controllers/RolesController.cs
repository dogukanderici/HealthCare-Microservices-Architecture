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
        private readonly RoleCreateEventPublisher _createPublisher;
        private readonly RoleUpdateEventPublisher _updatePublisher;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ILogger<RolesController> _logger;

        public RolesController(RoleCreateEventPublisher createPublisher, RoleUpdateEventPublisher updatePublisher, RoleManager<IdentityRole> roleManager, IMapper mapper, ILogger<RolesController> logger)
        {
            _createPublisher = createPublisher;
            _updatePublisher = updatePublisher;
            _roleManager = roleManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewRole(CreateRoleDto createRoleDto)
        {
            if (!await _roleManager.RoleExistsAsync(createRoleDto.RoleName))
            {
                IdentityRole role = new IdentityRole(createRoleDto.RoleName);

                IdentityResult result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    bool publiherResponse = await _createPublisher.PublishEventAsync(_mapper.Map<RoleCreatedEvent>(role));

                    _logger.LogInformation(LogConstant.LogMessageTemplate,
                        LogConstant.ServiceName,
                        nameof(RolesController),
                        nameof(AddNewRole),
                        LogConstant.SuccessMessages.RoleCreated);

                    return StatusCode(200, LogConstant.SuccessMessages.RoleCreated);
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

        [HttpPut]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto updateRoleDto)
        {
            var existedRole = await _roleManager.FindByIdAsync(Convert.ToString(updateRoleDto.Id));

            if (existedRole != null)
            {
                existedRole.Name = updateRoleDto.RoleName;

                IdentityResult result = await _roleManager.UpdateAsync(existedRole);

                if (result.Succeeded)
                {
                    bool publiherResponse = await _updatePublisher.PublishEventAsync(_mapper.Map<RoleUpdatedEvent>(existedRole));

                    _logger.LogInformation(LogConstant.LogMessageTemplate,
                        LogConstant.ServiceName,
                        nameof(RolesController),
                        nameof(UpdateRole),
                        LogConstant.SuccessMessages.RoleUpdated);

                    return StatusCode(200, LogConstant.SuccessMessages.RoleUpdated);
                }

                _logger.LogError(LogConstant.LogMessageTemplate,
                    LogConstant.ServiceName,
                    nameof(RolesController),
                    nameof(UpdateRole),
                    LogConstant.ErrorMessages.UpdateRoleFailed);

                return StatusCode(400, LogConstant.ErrorMessages.UpdateRoleFailed);
            }

            _logger.LogError(LogConstant.LogMessageTemplate,
                    LogConstant.ServiceName,
                    nameof(RolesController),
                    nameof(UpdateRole),
                    LogConstant.ErrorMessages.RoleNotFound);

            return StatusCode(400, LogConstant.ErrorMessages.RoleNotFound);
        }
    }
}
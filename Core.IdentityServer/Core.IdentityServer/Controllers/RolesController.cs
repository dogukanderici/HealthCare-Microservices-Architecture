using AutoMapper;
using Core.IdentityServer.Commons.Constants;
using Core.IdentityServer.Dtos.RoleDtos;
using Core.IdentityServer.Services.RabbitMQ.Events.Roles;
using Core.IdentityServer.Services.RabbitMQ.MessageBuses.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController
    {
        private readonly RoleCreateEventPublisher _createPublisher;
        private readonly RoleUpdateEventPublisher _updatePublisher;
        private readonly RoleSoftDeleteEventPublisher _deletePublisher;
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

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            if (!_roleManager.SupportsQueryableRoles)
            {
                return StatusCode(501, "Role table does not support queryable process!");
            }

            var roles = await _roleManager.Roles
               .AsNoTracking()
               .OrderBy(x => x.Name)
               .Select(x => new
               {
                   x.Id,
                   x.Name
               })
               .ToListAsync();

            if (!roles.Any())
            {
                return StatusCode(201, "Role table does not contain any role data!");
            }

            return StatusCode(200, roles);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            if (!_roleManager.SupportsQueryableRoles)
            {
                return StatusCode(501, "Role table does not support queryable process!");
            }

            var roleForId = await _roleManager.Roles
               .AsNoTracking()
               .Where(x => x.Id == id.ToString())
               .OrderBy(x => x.Name)
               .Select(x => new
               {
                   x.Id,
                   x.Name
               })
               .FirstOrDefaultAsync();

            if (roleForId == null)
            {
                return StatusCode(201, "Role not found!");
            }

            return StatusCode(200, roleForId);
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
            IdentityRole existedRole = await _roleManager.FindByIdAsync(Convert.ToString(updateRoleDto.Id));

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            IdentityRole existedRole = await _roleManager.FindByIdAsync(Convert.ToString(id));

            if (existedRole != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(existedRole);

                if (result.Succeeded)
                {
                    RoleSoftDeletedEvent deletedEvent = _mapper.Map<RoleSoftDeletedEvent>(existedRole);
                    deletedEvent.IsActive = false;

                    bool publiherResponse = await _deletePublisher.PublishEventAsync(deletedEvent);

                    _logger.LogInformation(LogConstant.LogMessageTemplate,
                        LogConstant.ServiceName,
                        nameof(RolesController),
                        nameof(DeleteRole),
                        LogConstant.SuccessMessages.RoleDeleted);

                    return StatusCode(200, LogConstant.SuccessMessages.RoleUpdated);
                }

                _logger.LogError(LogConstant.LogMessageTemplate,
                    LogConstant.ServiceName,
                    nameof(RolesController),
                    nameof(DeleteRole),
                    LogConstant.ErrorMessages.DeleteRoleFailed);

                return StatusCode(400, LogConstant.ErrorMessages.DeleteRoleFailed);
            }

            _logger.LogError(LogConstant.LogMessageTemplate,
                    LogConstant.ServiceName,
                    nameof(RolesController),
                    nameof(DeleteRole),
                    LogConstant.ErrorMessages.RoleNotFound);

            return StatusCode(400, LogConstant.ErrorMessages.RoleNotFound);
        }
    }
}
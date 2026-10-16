using HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Queries;
using HealthCare.Descriptions.WebAPI.Common.Helpers.ControllerHelpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "IdentityServerAccessToken", Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicRolesController : ControllerBase
    {
        private IControllerHelper<DynamicRolesController> _controllerHelper;
        private readonly IMediator _mediator;

        public DynamicRolesController(IControllerHelper<DynamicRolesController> controllerHelper, IMediator mediator)
        {
            _controllerHelper = controllerHelper;
            _mediator = mediator;
        }

        [HttpGet("{policyId}")]
        public async Task<IActionResult> GetRolesAsync(Guid policyId)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(new GetDynamicRolesQuery(policyId)),
                    nameof(GetRolesAsync));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleByIdAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(new GetDynamicRoleByIdQuery(id)),
                    nameof(GetRoleByIdAsync));
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetRolesByFilterAsync(GetDynamicRolesByFilterQuery query)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(query),
                    nameof(GetRoleByIdAsync));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync(CreateDynamicRoleCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(command),
                    nameof(CreateRoleAsync)
                );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoleAsync(UpdateDynamicRoleCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(command),
                    nameof(UpdateRoleAsync)
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(new DeleteDynamicRoleCommand(id)),
                    nameof(DeleteRoleAsync)
                );
        }
    }
}
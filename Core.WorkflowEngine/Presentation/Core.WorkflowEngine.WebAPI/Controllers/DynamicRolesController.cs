using Core.WorkflowEngine.Application.Features.Mediator.Commands.DynamicRoleCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.DynamicRoleQueries;
using Core.WorkflowEngine.WebAPI.Constants;
using Core.WorkflowEngine.WebAPI.Helpers.ControllerResponseHelpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.WorkflowEngine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicRolesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IControllerReponseHelper<DynamicRolesController> _controllerHelper;

        public DynamicRolesController(IMediator mediator, IControllerReponseHelper<DynamicRolesController> controllerHelper)
        {
            _mediator = mediator;
            _controllerHelper = controllerHelper;
        }

        [HttpGet("PolicyId")]
        public async Task<IActionResult> GetDynamicRolesAsync(Guid policyId)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(new GetDynamicRolesQuery(policyId)),
                    nameof(GetDynamicRolesAsync),
                    LogConstants.SuccessMessage.CallingSuccess,
                    LogConstants.ErrorMessage.CallingFail
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDynamicRoleByIdAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(new GetDynamicRoleByIdQuery(id)),
                    nameof(GetDynamicRoleByIdAsync),
                    LogConstants.SuccessMessage.CallingSuccess,
                    LogConstants.ErrorMessage.CallingFail
                );
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetDynamicRolesByFilterAsync(GetDynamicRolesByFilterQuery query)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(query),
                    nameof(GetDynamicRolesByFilterAsync),
                    LogConstants.SuccessMessage.CallingSuccess,
                    LogConstants.ErrorMessage.CallingFail
                );
        }

        [HttpPost]
        public async Task<IActionResult> CreateDyanmicRoleAsync(CreateDynamicRoleCommand query)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(query),
                    nameof(CreateDyanmicRoleAsync),
                    LogConstants.SuccessMessage.CallingSuccess,
                    LogConstants.ErrorMessage.CallingFail
                );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDyanmicRoleAsync(UpdateDynamicRoleCommand query)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(query),
                    nameof(UpdateDyanmicRoleAsync),
                    LogConstants.SuccessMessage.CallingSuccess,
                    LogConstants.ErrorMessage.CallingFail
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDyanmicRoleAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(new DeleteDynamicRoleCommand(id)),
                    nameof(DeleteDyanmicRoleAsync),
                    LogConstants.SuccessMessage.CallingSuccess,
                    LogConstants.ErrorMessage.CallingFail
                );
        }
    }
}
using Core.WorkflowEngine.Application.Features.Mediator.Commands.DynamicPolicyCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.DynamicPolicyQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.DynamicRoleQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.DynamicPolicyResults;
using Core.WorkflowEngine.WebAPI.Constants;
using Core.WorkflowEngine.WebAPI.Helpers.ControllerResponseHelpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.WorkflowEngine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicPoliciesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IControllerReponseHelper<DynamicPoliciesController> _controllerHelper;

        public DynamicPoliciesController(IMediator mediator, IControllerReponseHelper<DynamicPoliciesController> controllerHelper)
        {
            _mediator = mediator;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDynamicPoliciesAsync()
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(new GetDynamicPoliciesQuery()),
                    nameof(GetDynamicPoliciesAsync),
                    LogConstants.SuccessMessage.CallingSuccess,
                    LogConstants.ErrorMessage.CallingFail
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDynamicPolicyByIdAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(new GetDynamicPolicyByIdQuery(id)),
                    nameof(GetDynamicPolicyByIdAsync),
                    LogConstants.SuccessMessage.CallingSuccess,
                    LogConstants.ErrorMessage.CallingFail
                );
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetDynamicPoliciesByFilterAsync(GetDynamicPoliciesByFilterQuery query)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(query),
                    nameof(GetDynamicPoliciesByFilterAsync),
                    LogConstants.SuccessMessage.CallingSuccess,
                    LogConstants.ErrorMessage.CallingFail
                );
        }

        [HttpPost]
        public async Task<IActionResult> CreateDyanmicPolicyAsync(CreateDynamicPolicyCommand query)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(query),
                    nameof(CreateDyanmicPolicyAsync),
                    LogConstants.SuccessMessage.CallingSuccess,
                    LogConstants.ErrorMessage.CallingFail
                );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDyanmicPolicyAsync(UpdateDynamicPolicyCommand query)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(query),
                    nameof(UpdateDyanmicPolicyAsync),
                    LogConstants.SuccessMessage.CallingSuccess,
                    LogConstants.ErrorMessage.CallingFail
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDyanmicPolicyAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(new DeleteDynamicPolicyCommand(id)),
                    nameof(DeleteDyanmicPolicyAsync),
                    LogConstants.SuccessMessage.CallingSuccess,
                    LogConstants.ErrorMessage.CallingFail
                );
        }
    }
}
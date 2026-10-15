using HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Queries;
using HealthCare.Descriptions.WebAPI.Common.Helpers.ControllerHelpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicPoliciesController : BaseController
    {
        private IControllerHelper<DynamicPoliciesController> _controllerHelper;
        private readonly IMediator _mediator;

        public DynamicPoliciesController(IControllerHelper<DynamicPoliciesController> controllerHelper, IMediator mediator)
        {
            _controllerHelper = controllerHelper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPoliciesAsync()
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(new GetDynamicPoliciesQuery()),
                    nameof(GetPoliciesAsync));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicyByIdAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(new GetDynamicPolicyByIdQuery(id)),
                    nameof(GetPolicyByIdAsync));
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetPoliciesByFilterAsync(GetDynamicPoliciesByFilterQuery query)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(query),
                    nameof(GetPolicyByIdAsync));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePolicyAsync(CreateDynamicPolicyCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(command),
                    nameof(CreatePolicyAsync)
                );
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePolicyAsync(UpdateDynamicPolicyCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(command),
                    nameof(UpdatePolicyAsync)
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicyAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                    () => _mediator.Send(new DeleteDynamicPolicyCommand(id)),
                    nameof(DeletePolicyAsync)
                );
        }
    }
}
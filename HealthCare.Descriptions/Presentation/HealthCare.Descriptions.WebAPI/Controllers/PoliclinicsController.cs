using HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Queries;
using HealthCare.Descriptions.WebAPI.Common.Helpers.ControllerHelpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliclinicsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IControllerHelper<PoliclinicsController> _controllerHelper;

        public PoliclinicsController(IMediator mediator, IControllerHelper<PoliclinicsController> controllerHelper)
        {
            _mediator = mediator;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPoliclinicsAsync()
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetPoliclinicsQuery()),
                nameof(GetPoliclinicsAsync)
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPoliclinicByIdAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetPoliclinicByIdQuery(id)),
                nameof(GetPoliclinicByIdAsync)
                );
        }

        [HttpPost("query")]
        public async Task<IActionResult> GetPoliclinicsByFilterAsync(GetPoliclinicsByFilterQuery query)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(query),
                nameof(GetPoliclinicsByFilterAsync)
                );
        }

        [HttpPost]
        public async Task<IActionResult> CreatePoliclinicAsync(CreatePoliclinicCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(command),
                nameof(CreatePoliclinicAsync)
                );
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePoliclinicAsync(UpdatePoliclinicCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(command),
                nameof(UpdatePoliclinicAsync)
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoliclinicAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new RemovePoliclinicCommand(id)),
                nameof(DeletePoliclinicAsync)
                );
        }
    }
}
using HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Queries;
using HealthCare.Descriptions.WebAPI.Common.Helpers.ControllerHelpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IControllerHelper<ServiceTypesController> _controllerHelper;

        public ServiceTypesController(IMediator mediator, IControllerHelper<ServiceTypesController> controllerHelper)
        {
            _mediator = mediator;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetServiceTypesAsync()
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetServiceTypesQuery()),
                nameof(GetServiceTypesAsync)
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceTypeByIdAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetServiceTypeByIdQuery(id)),
                nameof(GetServiceTypeByIdAsync)
                );
        }

        [HttpPost("query")]
        public async Task<IActionResult> GetServiceTypesByFilterAsync(GetServiceTypesByFilterQuery query)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(query),
                nameof(GetServiceTypesByFilterAsync)
                );
        }

        [HttpPost]
        public async Task<IActionResult> CreateServiceTypeAsync(CreateServiceTypeCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(command),
                nameof(CreateServiceTypeAsync)
                );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateServiceTypeAsync(UpdateServiceTypeCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(command),
                nameof(UpdateServiceTypeAsync)
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceTypeAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new RemoveServiceTypeCommand(id)),
                nameof(DeleteServiceTypeAsync)
                );
        }
    }
}
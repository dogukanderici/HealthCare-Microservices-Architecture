using HealthCare.Descriptions.Application.Features.Mediators.Districts.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.Districts.Queries;
using HealthCare.Descriptions.WebAPI.Common.Helpers.ControllerHelpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IControllerHelper<DistrictsController> _controllerHelper;

        public DistrictsController(IMediator mediator, IControllerHelper<DistrictsController> controllerHelper)
        {
            _mediator = mediator;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDistrictsAsync()
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetDistrictsQuery()),
                nameof(GetDistrictsAsync)
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDistrictByIdAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetDistrictByIdQuery(id)),
                nameof(GetDistrictByIdAsync)
                );
        }

        [HttpPost("query")]
        public async Task<IActionResult> GetDistrictsByFilterAsync(GetDistrictsByFilterQuery query)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(query),
                nameof(GetDistrictsByFilterAsync)
                );
        }

        [HttpPost]
        public async Task<IActionResult> CreateDistrictAsync(CreateDistrictCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(command),
                nameof(CreateDistrictAsync)
                );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDistrictAsync(UpdateDistrictCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(command),
                nameof(UpdateDistrictAsync)
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistrictAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new RemoveDistrictCommand(id)),
                nameof(DeleteDistrictAsync)
                );
        }
    }
}
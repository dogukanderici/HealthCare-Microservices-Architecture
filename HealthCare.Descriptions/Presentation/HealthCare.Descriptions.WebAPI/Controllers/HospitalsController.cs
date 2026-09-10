using HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Queries;
using HealthCare.Descriptions.WebAPI.Common.Helpers.ControllerHelpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IControllerHelper<HospitalsController> _controllerHelper;

        public HospitalsController(IMediator mediator, IControllerHelper<HospitalsController> controllerHelper)
        {
            _mediator = mediator;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetHospitalsAsync()
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetHospitalsQuery()),
                nameof(GetHospitalsAsync)
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHospitalByIdAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetHospitalByIdQuery(id)),
                nameof(GetHospitalByIdAsync)
                );
        }

        [HttpPost("query")]
        public async Task<IActionResult> GetHospitalsByFilterAsync(GetHospitalsByFilterQuery query)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(query),
                nameof(GetHospitalsByFilterAsync)
                );
        }

        [HttpPost]
        public async Task<IActionResult> CreateHospitalAsync(CreateHospitalCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(command),
                nameof(CreateHospitalAsync)
                );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHospitalAsync(UpdateHospitalCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(command),
                nameof(UpdateHospitalAsync)
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospitalAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new RemoveHospitalCommand(id)),
                nameof(DeleteHospitalAsync)
                );
        }
    }
}
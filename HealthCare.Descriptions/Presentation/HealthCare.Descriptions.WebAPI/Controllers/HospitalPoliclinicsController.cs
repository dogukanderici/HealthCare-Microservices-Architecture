using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Queries;
using HealthCare.Descriptions.WebAPI.Common.Helpers.ControllerHelpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalPoliclinicsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IControllerHelper<HospitalPoliclinicsController> _controllerHelper;

        public HospitalPoliclinicsController(IMediator mediator, IControllerHelper<HospitalPoliclinicsController> controllerHelper)
        {
            _mediator = mediator;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetHospitalPoliclinicsAsync()
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetHospitalPoliclinicsQuery()),
                nameof(GetHospitalPoliclinicsAsync)
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHospitalPoliclinicByIdAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetHospitalPoliclinicByIdQuery(id)),
                nameof(GetHospitalPoliclinicByIdAsync)
                );
        }

        [HttpPost("query")]
        public async Task<IActionResult> GetHospitalPoliclinicsByFilterAsync(GetHospitalPoliclinicsByFilterQuery query)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(query),
                nameof(GetHospitalPoliclinicsByFilterAsync)
                );
        }

        [HttpPost]
        public async Task<IActionResult> CreateHospitalPoliclinicAsync(CreateHospitalPoliclinicCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(command),
                nameof(CreateHospitalPoliclinicAsync)
                );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHospitalPoliclinicAsync(UpdateHospitalPoliclinicCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(command),
                nameof(UpdateHospitalPoliclinicAsync)
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospitalPoliclinicAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new RemoveHospitalPoliclinicCommand(id)),
                nameof(DeleteHospitalPoliclinicAsync)
                );
        }
    }
}
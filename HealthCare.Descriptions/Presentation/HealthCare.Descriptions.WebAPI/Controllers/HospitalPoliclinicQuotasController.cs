using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Queries;
using HealthCare.Descriptions.WebAPI.Common.Helpers.ControllerHelpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalPoliclinicQuotaQuotasController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IControllerHelper<HospitalPoliclinicQuotaQuotasController> _controllerHelper;

        public HospitalPoliclinicQuotaQuotasController(IMediator mediator, IControllerHelper<HospitalPoliclinicQuotaQuotasController> controllerHelper)
        {
            _mediator = mediator;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetHospitalPoliclinicQuotasAsync()
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetHospitalPoliclinicQuotasQuery()),
                nameof(GetHospitalPoliclinicQuotasAsync)
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHospitalPoliclinicQuotaByIdAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetHospitalPoliclinicQuotaByIdQuery(id)),
                nameof(GetHospitalPoliclinicQuotaByIdAsync)
                );
        }

        [HttpPost("query")]
        public async Task<IActionResult> GetHospitalPoliclinicQuotasByFilterAsync(GetHospitalPoliclinicQuotasByFilterQuery query)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(query),
                nameof(GetHospitalPoliclinicQuotasByFilterAsync)
                );
        }

        [HttpPost]
        public async Task<IActionResult> CreateHospitalPoliclinicQuotaAsync(CreateHospitalPoliclinicQuotaCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(command),
                nameof(CreateHospitalPoliclinicQuotaAsync)
                );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHospitalPoliclinicQuotaAsync(UpdateHospitalPoliclinicQuotaCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(command),
                nameof(UpdateHospitalPoliclinicQuotaAsync)
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospitalPoliclinicQuotaAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new RemoveHospitalPoliclinicQuotaCommand(id)),
                nameof(DeleteHospitalPoliclinicQuotaAsync)
                );
        }
    }
}
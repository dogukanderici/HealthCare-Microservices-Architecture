using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Queries;
using HealthCare.Descriptions.WebAPI.Common.Helpers.ControllerHelpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentStatusesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IControllerHelper<AppointmentStatusesController> _controllerHelper;

        public AppointmentStatusesController(IMediator mediator, IControllerHelper<AppointmentStatusesController> controllerHelper)
        {
            _mediator = mediator;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointmentStatuses()
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetAppointmentStatusesQuery()),
                nameof(GetAppointmentStatuses));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentStatusById(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetAppointmentStatusByIdQuery(id)),
                nameof(GetAppointmentStatusById));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointmentStatus(CreateAppointmentStatusCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(command),
                nameof(CreateAppointmentStatus));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAppointmentStatus(UpdateAppointmentStatusCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(command),
                nameof(UpdateAppointmentStatus));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointmentStatus(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new RemoveAppointmentStatusCommand(id)),
                nameof(DeleteAppointmentStatus));
        }
    }
}
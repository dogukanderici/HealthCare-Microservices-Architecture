using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentStatusesController : BaseController
    {
        private readonly IMediator _mediator;

        public AppointmentStatusesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointmentStatuses()
        {
            InternalHandlerResponse<IReadOnlyCollection<GetAppointmentStatusesResult>> handlerResponse = await _mediator.Send(new GetAppointmentStatusesQuery());

            if (handlerResponse.IsSuccess)
            {
                return Ok(handlerResponse.Data);
            }

            return BadRequest(handlerResponse.InternalMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentStatusById(Guid id)
        {
            InternalHandlerResponse<GetAppointmentStatusByIdResult> handlerResponse = await _mediator.Send(new GetAppointmentStatusByIdQuery(id));

            if (handlerResponse.IsSuccess)
            {
                return Ok(handlerResponse.Data);
            }

            return BadRequest(handlerResponse.InternalMessage);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointmentStatus(CreateAppointmentStatusCommand command)
        {
            InternalHandlerResponse<Guid> handlerResponse = await _mediator.Send(command);

            if (handlerResponse.IsSuccess)
            {
                return Ok(handlerResponse.Data);
            }

            return BadRequest(handlerResponse.InternalMessage);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAppointmentStatus(UpdateAppointmentStatusCommand command)
        {
            InternalHandlerResponse<DateTimeOffset> handlerResponse = await _mediator.Send(command);

            if (handlerResponse.IsSuccess)
            {
                return Ok(handlerResponse.Data);
            }

            return BadRequest(handlerResponse.InternalMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointmentStatus(Guid id)
        {
            InternalHandlerResponse<bool> handlerResponse = await _mediator.Send(new RemoveAppointmentStatusCommand(id));

            if (handlerResponse.IsSuccess)
            {
                return Ok(handlerResponse.Data);
            }

            return BadRequest(handlerResponse.InternalMessage);
        }
    }
}
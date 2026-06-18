using HealthCare.Descriptions.Application.Features.Mediator.Commands.AppointmentStatusCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.AppointmentStatusQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.AppointmentStatusResults;
using HealthCare.Descriptions.WebAPI.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentStatusesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AppointmentStatusesController> _logger;

        public AppointmentStatusesController(IMediator mediator, ILogger<AppointmentStatusesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointmentStatusesData()
        {
            try
            {
                List<GetAppointmentStatusesQueryResult> result = await _mediator.Send(new GetAppointmentStatusesQuery());

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(AppointmentStatusesController),
                    nameof(GetAppointmentStatusesData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetAppointmentStatusesQueryResult>
                {
                    StatusCode = 200,
                    Message = "Success",
                    TimeStamp = DateTime.UtcNow,
                    Datas = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(AppointmentStatusesController),
                    nameof(GetAppointmentStatusesData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentStatusByIdData(Guid id)
        {
            try
            {
                GetAppointmentStatusByIdQueryResult result = await _mediator.Send(new GetAppointmentStatusByIdQuery(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(AppointmentStatusesController),
                    nameof(GetAppointmentStatusesData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetAppointmentStatusByIdQueryResult>
                {
                    StatusCode = 200,
                    Message = "Success",
                    TimeStamp = DateTime.UtcNow,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(AppointmentStatusesController),
                    nameof(GetAppointmentStatusByIdData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointmentStatusData(CreateAppointmentStatusCommand createAppointmentStatusCommand)
        {
            try
            {
                createAppointmentStatusCommand.CreatedBy = Guid.NewGuid();
                createAppointmentStatusCommand.CreatedAt = DateTimeOffset.UtcNow;
                createAppointmentStatusCommand.UpdatedBy = Guid.NewGuid();
                createAppointmentStatusCommand.UpdatedAt = DateTimeOffset.UtcNow;

                await _mediator.Send(createAppointmentStatusCommand);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(AppointmentStatusesController),
                    nameof(CreateAppointmentStatusData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.DataCreated,
                    TimeStamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(AppointmentStatusesController),
                    nameof(CreateAppointmentStatusData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAppointmentStatusData(UpdateAppointmentStatusCommand updateAppointmentStatusCommand)
        {
            try
            {
                if (updateAppointmentStatusCommand.CreatedAt == DateTimeOffset.MinValue ||
                    updateAppointmentStatusCommand.CreatedBy == Guid.Empty)
                {
                    var result = await _mediator.Send(new GetAppointmentStatusByIdQuery(updateAppointmentStatusCommand.Id));

                    updateAppointmentStatusCommand.CreatedBy = result.CreatedBy;
                    updateAppointmentStatusCommand.CreatedAt = result.CreatedAt;
                }

                updateAppointmentStatusCommand.UpdatedBy = Guid.NewGuid();
                updateAppointmentStatusCommand.UpdatedAt = DateTimeOffset.UtcNow;

                await _mediator.Send(updateAppointmentStatusCommand);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(AppointmentStatusesController),
                    nameof(CreateAppointmentStatusData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.DataUpdated,
                    TimeStamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(AppointmentStatusesController),
                    nameof(UpdateAppointmentStatusData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAppointmentStatusData(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteAppointmentStatusCommand(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(AppointmentStatusesController),
                    nameof(CreateAppointmentStatusData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.DataDelete,
                    TimeStamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(AppointmentStatusesController),
                    nameof(DeleteAppointmentStatusData),
                    ApiResponseMessageConstant.CallingSuccess);

                throw;
            }
        }
    }
}

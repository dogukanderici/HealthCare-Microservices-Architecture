using HealthCare.Operations.Application.Features.Mediator.Commands.AppoinmentCommands;
using HealthCare.Operations.Application.Features.Mediator.Queries.AppoinmentQueries;
using HealthCare.Operations.Application.Features.Mediator.Results.AppoinmentResults;
using HealthCare.Operations.WebAPI.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Specialized;

namespace HealthCare.Operations.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AppointmentsController> _logger;

        public AppointmentsController(IMediator mediator, ILogger<AppointmentsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointmentsData()
        {
            try
            {
                List<GetAppointmentsQueryResult> result = await _mediator.Send(new GetAppointmentsQuery());

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(AppointmentsController),
                    nameof(GetAppointmentsData),
                    ApiResponseMessageConstant.Success);

                return Ok(new GenericApiResponse<GetAppointmentsQueryResult>
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.CallingSuccess,
                    TimeStamp = DateTime.UtcNow,
                    Datas = result
                });
            }
            catch (Exception ex)
            {

                _logger.LogError("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(AppointmentsController),
                    nameof(GetAppointmentsData),
                    ApiResponseMessageConstant.Fail);

                throw;

            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentByIdData(Guid id)
        {
            try
            {
                GetAppointmentByIdQueryResult result = await _mediator.Send(new GetAppointmentByIdQuery(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(AppointmentsController),
                    nameof(GetAppointmentByIdData),
                    ApiResponseMessageConstant.Success);

                return Ok(new GenericApiResponse<GetAppointmentByIdQueryResult>
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.CallingSuccess,
                    TimeStamp = DateTime.UtcNow,
                    Data = result
                });
            }
            catch (Exception ex)
            {

                _logger.LogError("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(AppointmentsController),
                    nameof(GetAppointmentByIdData),
                    ApiResponseMessageConstant.Fail);

                throw;

            }
        }

        [HttpGet("params/{parameters}")]
        public async Task<IActionResult> GetAppointmentsByFilterData(string parameters)
        {
            try
            {

                NameValueCollection queryParameters = System.Web.HttpUtility.ParseQueryString(parameters);

                string? NameSurname = queryParameters["NameSurname"];
                bool? Nationality = bool.TryParse(queryParameters["Nationality"], out bool checkNationality) ? checkNationality : null;
                string? IDNumber = queryParameters["IDNumber"];
                string? Phone = queryParameters["Phone"];
                string? Email = queryParameters["Email"];
                Guid? HospitalId = Guid.TryParse(queryParameters["HospitalId"], out Guid checkHospitalId) ? checkHospitalId : null;
                Guid? PoliclinicId = Guid.TryParse(queryParameters["PoliclinicId"], out Guid checkPoliclinicId) ? checkPoliclinicId : null;
                int? City = int.TryParse(queryParameters["City"], out int checkCity) ? checkCity : null;
                Guid? District = Guid.TryParse(queryParameters["District"], out Guid checkDistrict) ? checkDistrict : null;
                DateTimeOffset? AppointmentDate = DateTimeOffset.TryParse(
                    queryParameters["AppointmentDate"], out DateTimeOffset checkAppointmentDate) ? checkAppointmentDate : null;
                bool? IsClosed = bool.TryParse(queryParameters["IsClosed"], out bool checkIsClosed) ? checkIsClosed : null;

                GetAppointmentsByFilterQuery queryParams = GetAppointmentsByFilterQuery.Filter(
                    NameSurname, Nationality, IDNumber, Phone, Email, HospitalId, PoliclinicId, City, District, AppointmentDate, IsClosed);

                List<GetAppointmentsByFilterQueryResult> result = await _mediator.Send(queryParams);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                            nameof(AppointmentsController),
                            nameof(GetAppointmentsByFilterData),
                            ApiResponseMessageConstant.Success);

                return Ok(new GenericApiResponse<GetAppointmentsByFilterQueryResult>
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.CallingSuccess,
                    TimeStamp = DateTime.UtcNow,
                    Datas = result
                });
            }
            catch (Exception ex)
            {

                _logger.LogError("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(AppointmentsController),
                    nameof(GetAppointmentsByFilterData),
                    ApiResponseMessageConstant.Fail);

                throw;

            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointmentData(CreateAppointmentCommand createAppointmentCommand)
        {
            try
            {
                createAppointmentCommand.CreatedAt = DateTimeOffset.UtcNow;
                createAppointmentCommand.CreatedBy = Guid.NewGuid();
                createAppointmentCommand.UpdatedAt = DateTimeOffset.UtcNow;
                createAppointmentCommand.UpdatedBy = Guid.NewGuid();

                await _mediator.Send(createAppointmentCommand);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                            nameof(AppointmentsController),
                            nameof(CreateAppointmentData),
                            ApiResponseMessageConstant.Success);

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
                    nameof(AppointmentsController),
                    nameof(CreateAppointmentData),
                    ApiResponseMessageConstant.Fail);

                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAppointmentData(UpdateAppointmentCommand updateAppointmentCommand)
        {
            try
            {
                if (updateAppointmentCommand.CreatedAt == DateTimeOffset.MinValue &&
                    updateAppointmentCommand.CreatedBy == Guid.Empty)
                {
                    GetAppointmentByIdQueryResult result = await _mediator.Send(new GetAppointmentByIdQuery(updateAppointmentCommand.Id));

                    if (result != null)
                    {
                        updateAppointmentCommand.CreatedAt = result.CreatedAt;
                        updateAppointmentCommand.CreatedBy = result.CreatedBy;
                    }
                    else
                    {
                        updateAppointmentCommand.CreatedAt = DateTimeOffset.UtcNow;
                        updateAppointmentCommand.CreatedBy = Guid.NewGuid();
                    }
                }

                updateAppointmentCommand.UpdatedAt = DateTimeOffset.UtcNow;
                updateAppointmentCommand.UpdatedBy = Guid.NewGuid();

                await _mediator.Send(updateAppointmentCommand);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                            nameof(AppointmentsController),
                            nameof(UpdateAppointmentData),
                            ApiResponseMessageConstant.Success);

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
                    nameof(AppointmentsController),
                    nameof(UpdateAppointmentData),
                    ApiResponseMessageConstant.Fail);

                throw;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAppointmentData(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteAppointmentCommand(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                            nameof(AppointmentsController),
                            nameof(DeleteAppointmentData),
                            ApiResponseMessageConstant.Success);

                return Ok(new GenericApiResponse
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.DataDeleted,
                    TimeStamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(AppointmentsController),
                    nameof(DeleteAppointmentData),
                    ApiResponseMessageConstant.Fail);

                throw;
            }
        }
    }
}

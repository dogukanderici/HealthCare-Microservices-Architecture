using HealthCare.Operations.Application.Features.Mediator.Commands.BillCommands;
using HealthCare.Operations.Application.Features.Mediator.Handlers.Wrappers;
using HealthCare.Operations.Application.Features.Mediator.Queries.BillQueries;
using HealthCare.Operations.Application.Features.Mediator.Results.BillResults;
using HealthCare.Operations.WebAPI.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Specialized;

namespace HealthCare.Operations.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BillsController> _logger;

        public BillsController(IMediator mediator, ILogger<BillsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetBillsData()
        {
            try
            {
                List<GetBillsQueryResult> datas = await _mediator.Send(new GetBillsQuery());

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(BillsController),
                    nameof(GetBillsData),
                    ApiResponseMessageConstant.CallingSuccess
                    );

                return Ok(new GenericApiResponse<GetBillsQueryResult>
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.Success,
                    TimeStamp = DateTime.UtcNow,
                    Datas = datas
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(BillsController),
                    nameof(GetBillsData),
                    ApiResponseMessageConstant.CallingFail
                    );

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillsData(Guid id)
        {
            try
            {
                GetBillByIdQueryResult data = await _mediator.Send(new GetBillByIdQuery(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(BillsController),
                    nameof(GetBillsData),
                    ApiResponseMessageConstant.CallingSuccess
                    );

                return Ok(new GenericApiResponse<GetBillByIdQueryResult>
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.Success,
                    TimeStamp = DateTime.UtcNow,
                    Data = data
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(BillsController),
                    nameof(GetBillsData),
                    ApiResponseMessageConstant.CallingFail
                    );

                throw;
            }
        }

        [HttpGet("params/{parameters}")]
        public async Task<IActionResult> GetBillsByFilterData(string parameters)
        {
            try
            {
                NameValueCollection queryParameters = System.Web.HttpUtility.ParseQueryString(parameters);

                Guid? AppointmentId = Guid.TryParse(queryParameters["AppointmentId"], out Guid checkAppointmentId) ? checkAppointmentId : null;
                string? BillingNumber = queryParameters["BillingNumber"];
                DateTimeOffset? BillingDate = DateTimeOffset.TryParse(queryParameters["BillingDate"], out DateTimeOffset checkBillingDate) ? checkBillingDate : null;
                string? PatientNameSurname = queryParameters["PatientNameSurname"];
                string? PatientPhone = queryParameters["PatientPhone"];
                bool? PatientNationality = bool.TryParse(queryParameters["PatientNationality"], out bool checkPatientNationality) ? checkPatientNationality : null;
                string? PatientIDNumber = queryParameters["PatientIDNumber"];
                string? PatientMail = queryParameters["PatientMail"];

                GetBillsByFilterQuery filter = GetBillsByFilterQuery.Filter(
                    AppointmentId,
                    BillingNumber,
                    BillingDate,
                    PatientNameSurname,
                    PatientPhone,
                    PatientNationality,
                    PatientIDNumber,
                    PatientMail
                    );

                List<GetBillsByFilterQueryResult> datas = await _mediator.Send(filter);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(BillsController),
                    nameof(GetBillsByFilterData),
                    ApiResponseMessageConstant.CallingSuccess
                    );

                return Ok(new GenericApiResponse<GetBillsByFilterQueryResult>
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.Success,
                    TimeStamp = DateTime.UtcNow,
                    Datas = datas
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(BillsController),
                    nameof(GetBillsByFilterData),
                    ApiResponseMessageConstant.CallingFail
                    );

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBillData(CreateBillWithBillingServiceCommand createBillWithBillingServiceCommand)
        {
            try
            {
                createBillWithBillingServiceCommand.Id = Guid.NewGuid();
                createBillWithBillingServiceCommand.CreatedAt = DateTime.UtcNow;
                createBillWithBillingServiceCommand.CreatedBy = Guid.NewGuid();
                createBillWithBillingServiceCommand.UpdatedBy = Guid.NewGuid();
                createBillWithBillingServiceCommand.UpdatedAt = DateTime.UtcNow;

                GenericHandlerResponse mediatorResponse = await _mediator.Send(createBillWithBillingServiceCommand);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(BillsController),
                    nameof(CreateBillData),
                    ApiResponseMessageConstant.DataCreated
                    );

                if (!mediatorResponse.HandlerStatus)
                {
                    throw new Exception(mediatorResponse.HandlerMessage);
                }

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
                    nameof(BillsController),
                    nameof(CreateBillData),
                    ApiResponseMessageConstant.CallingFail
                    );

                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBillData(UpdateBillCommand updateBillCommand)
        {
            try
            {
                if (updateBillCommand.CreatedAt == DateTimeOffset.MinValue &&
                    updateBillCommand.CreatedBy == Guid.Empty)
                {
                    GetBillByIdQueryResult result = await _mediator.Send(new GetBillByIdQuery(updateBillCommand.Id));

                    if (result != null)
                    {
                        updateBillCommand.CreatedAt = result.CreatedAt;
                        updateBillCommand.CreatedBy = result.CreatedBy;
                    }
                    else
                    {
                        updateBillCommand.CreatedAt = DateTime.UtcNow;
                        updateBillCommand.CreatedBy = Guid.NewGuid();
                    }
                }

                updateBillCommand.UpdatedBy = Guid.NewGuid();
                updateBillCommand.UpdatedAt = DateTime.UtcNow;

                await _mediator.Send(updateBillCommand);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(BillsController),
                    nameof(UpdateBillData),
                    ApiResponseMessageConstant.Success
                    );

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
                    nameof(BillsController),
                    nameof(UpdateBillData),
                    ApiResponseMessageConstant.CallingFail
                    );

                throw;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBillData(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteBillCommand(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                   nameof(BillsController),
                   nameof(DeleteBillData),
                   ApiResponseMessageConstant.Success
                   );

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
                    nameof(BillsController),
                    nameof(DeleteBillData),
                    ApiResponseMessageConstant.CallingFail
                    );

                throw;
            }
        }
    }
}

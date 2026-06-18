using HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalPoliclinicCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalPoliclinicQuotaCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.HospitalPoliclinicQuotaQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalPoliclinicQuotaResults;
using HealthCare.Descriptions.WebAPI.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Specialized;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalPoliclinicQuotasController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<HospitalPoliclinicQuotasController> _logger;

        public HospitalPoliclinicQuotasController(IMediator mediator, ILogger<HospitalPoliclinicQuotasController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetHospitalPoliclinicQuotasData()
        {
            try
            {
                List<GetHospitalPoliclinicQuotasQueryResult> result = await _mediator.Send(new GetHospitalPoliclinicQuotasQuery());

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalPoliclinicQuotasController),
                    nameof(GetHospitalPoliclinicQuotasData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetHospitalPoliclinicQuotasQueryResult>
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.Success,
                    TimeStamp = DateTime.UtcNow,
                    Datas = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalPoliclinicQuotasController),
                    nameof(GetHospitalPoliclinicQuotasData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHospitalPoliclinicQuotaByIdData(Guid id)
        {
            try
            {
                GetHospitalPoliclinicQuotaByIdQueryResult result = await _mediator.Send(new GetHospitalPoliclinicQuotaByIdQuery(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalPoliclinicQuotasController),
                    nameof(GetHospitalPoliclinicQuotaByIdData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetHospitalPoliclinicQuotaByIdQueryResult>
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.Success,
                    TimeStamp = DateTime.UtcNow,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalPoliclinicQuotasController),
                    nameof(GetHospitalPoliclinicQuotaByIdData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpGet("params/{parameters}")]
        public async Task<IActionResult> GetHospitalPoliclinicQuotasByFilterData(string parameters)
        {
            try
            {
                NameValueCollection queryParameters = System.Web.HttpUtility.ParseQueryString(parameters);

                Guid? QuotaType = Guid.TryParse(queryParameters["QuotaType"], out Guid checkQuotaType) ? checkQuotaType : null;

                DateTimeOffset? ValidityDate = DateTimeOffset.TryParse(queryParameters["ValidityDate"], out DateTimeOffset checkValidityDate) ?
                    checkValidityDate : null;

                bool? IsAvailable = bool.TryParse(queryParameters["IsAvailable"], out bool checkIsAvailable) ? checkIsAvailable : null;

                GetHospitalPoliclinicQuotasByFilterQuery queryParameter = GetHospitalPoliclinicQuotasByFilterQuery.Filter(QuotaType, ValidityDate, IsAvailable);

                List<GetHospitalPoliclinicQuotasByFilterQueryResult> result = await _mediator.Send(queryParameter);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalPoliclinicQuotasController),
                    nameof(GetHospitalPoliclinicQuotasByFilterData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetHospitalPoliclinicQuotasByFilterQueryResult>
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.Success,
                    TimeStamp = DateTime.UtcNow,
                    Datas = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalPoliclinicQuotasController),
                    nameof(GetHospitalPoliclinicQuotasByFilterData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateHospitalPoliclinicQuotaData(CreateHospitalPoliclinicQuotaCommand createHospitalPoliclinicQuotaCommand)
        {
            try
            {
                createHospitalPoliclinicQuotaCommand.CreatedAt = DateTimeOffset.UtcNow;
                createHospitalPoliclinicQuotaCommand.CreatedBy = Guid.NewGuid();
                createHospitalPoliclinicQuotaCommand.UpdatedAt = DateTimeOffset.UtcNow;
                createHospitalPoliclinicQuotaCommand.UpdatedBy = Guid.NewGuid();

                await _mediator.Send(createHospitalPoliclinicQuotaCommand);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalPoliclinicQuotasController),
                    nameof(CreateHospitalPoliclinicQuotaData),
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
                    nameof(HospitalPoliclinicQuotasController),
                    nameof(CreateHospitalPoliclinicQuotaData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateHospitalPoliclinicQuotaData(UpdateHospitalPoliclinicQuotaCommand updateHospitalPoliclinicQuotaCommand)
        {
            try
            {
                if (updateHospitalPoliclinicQuotaCommand.CreatedAt == DateTimeOffset.MinValue &&
                    updateHospitalPoliclinicQuotaCommand.CreatedBy == Guid.Empty)
                {
                    GetHospitalPoliclinicQuotaByIdQueryResult result = await _mediator.Send(
                        new GetHospitalPoliclinicQuotaByIdQuery(updateHospitalPoliclinicQuotaCommand.Id)
                        );

                    updateHospitalPoliclinicQuotaCommand.CreatedAt = result.CreatedAt;
                    updateHospitalPoliclinicQuotaCommand.CreatedBy = result.CreatedBy;
                }

                updateHospitalPoliclinicQuotaCommand.UpdatedAt = DateTimeOffset.UtcNow;
                updateHospitalPoliclinicQuotaCommand.UpdatedBy = Guid.NewGuid();

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalPoliclinicQuotasController),
                    nameof(UpdateHospitalPoliclinicQuotaData),
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
                    nameof(HospitalPoliclinicQuotasController),
                    nameof(UpdateHospitalPoliclinicQuotaData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHosptialPoliclinicQuotaData(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteHospitalPoliclinicQuotaCommand(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalPoliclinicQuotasController),
                    nameof(DeleteHosptialPoliclinicQuotaData),
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
                    nameof(HospitalPoliclinicQuotasController),
                    nameof(DeleteHosptialPoliclinicQuotaData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }
    }
}

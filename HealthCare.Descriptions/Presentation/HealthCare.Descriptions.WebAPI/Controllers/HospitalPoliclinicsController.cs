using HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalPoliclinicCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.HospitalPoliclinicQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalPoliclinicResults;
using HealthCare.Descriptions.WebAPI.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalPoliclinicsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<HospitalPoliclinicsController> _logger;

        public HospitalPoliclinicsController(IMediator mediator, ILogger<HospitalPoliclinicsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetHospitalPoliclinicsData()
        {
            try
            {
                List<GetHospitalPoliclinicsQueryResult> result = await _mediator.Send(new GetHospitalPoliclinicsQuery());

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalPoliclinicsController),
                    nameof(GetHospitalPoliclinicsData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetHospitalPoliclinicsQueryResult>
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
                    nameof(HospitalPoliclinicsController),
                    nameof(GetHospitalPoliclinicsData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHospitalPoliclinicByIdData(Guid id)
        {
            try
            {
                GetHospitalPoliclinicByIdQueryResult result = await _mediator.Send(new GetHospitalPoliclinicByIdQuery(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalPoliclinicsController),
                    nameof(GetHospitalPoliclinicByIdData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetHospitalPoliclinicByIdQueryResult>
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
                    nameof(HospitalPoliclinicsController),
                    nameof(GetHospitalPoliclinicByIdData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpGet("params/{parameters}")]
        public async Task<IActionResult> GetHospitalPoliclinicsByFilterData(string parameters)
        {
            try
            {
                NameValueCollection queryParameters = System.Web.HttpUtility.ParseQueryString(parameters);

                Guid? hospitalId = Guid.TryParse(queryParameters["HospitalId"], out Guid checkHospitalId) ? checkHospitalId : null;
                Guid? policlinicId = Guid.TryParse(queryParameters["PoliclinicId"], out Guid checkPoliclinicId) ? checkPoliclinicId : null;
                bool? isAvailable = bool.TryParse(queryParameters["IsAvailable"], out bool chekcIsAvailable) ? chekcIsAvailable : null;

                GetHospitalPoliclinicsByFilterQuery queryParameter = GetHospitalPoliclinicsByFilterQuery.Filter(hospitalId, policlinicId, isAvailable);

                List<GetHospitalPoliclinicsByFilterQueryResult> result = await _mediator.Send(queryParameter);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalPoliclinicsController),
                    nameof(GetHospitalPoliclinicByIdData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetHospitalPoliclinicsByFilterQueryResult>
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
                    nameof(HospitalPoliclinicsController),
                    nameof(GetHospitalPoliclinicByIdData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateHospitalPoliclinicData(CreateHospitalPoliclinicCommand createHospitalPoliclinicCommand)
        {
            try
            {
                createHospitalPoliclinicCommand.CreatedAt = DateTimeOffset.UtcNow;
                createHospitalPoliclinicCommand.CreatedBy = Guid.NewGuid();
                createHospitalPoliclinicCommand.UpdatedAt = DateTimeOffset.UtcNow;
                createHospitalPoliclinicCommand.UpdatedBy = Guid.NewGuid();

                await _mediator.Send(createHospitalPoliclinicCommand);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalPoliclinicsController),
                    nameof(CreateHospitalPoliclinicData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.DataCreated,
                    TimeStamp = DateTime.UtcNow,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalPoliclinicsController),
                    nameof(CreateHospitalPoliclinicData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHospitalPoliclinicData(UpdateHospitalPoliclinicCommand updateHospitalPoliclinicCommand)
        {
            try
            {
                if (updateHospitalPoliclinicCommand.CreatedAt == DateTimeOffset.MinValue &&
                    updateHospitalPoliclinicCommand.CreatedBy == Guid.Empty)
                {
                    GetHospitalPoliclinicByIdQueryResult result = await _mediator.Send(new GetHospitalPoliclinicByIdQuery(updateHospitalPoliclinicCommand.Id));

                    if (result != null)
                    {
                        updateHospitalPoliclinicCommand.CreatedAt = result.CreatedAt;
                        updateHospitalPoliclinicCommand.CreatedBy = result.CreatedBy;
                    }
                    else
                    {
                        updateHospitalPoliclinicCommand.CreatedAt = DateTimeOffset.UtcNow;
                        updateHospitalPoliclinicCommand.CreatedBy = Guid.NewGuid();
                    }
                }

                updateHospitalPoliclinicCommand.UpdatedAt = DateTimeOffset.UtcNow;
                updateHospitalPoliclinicCommand.UpdatedBy = Guid.NewGuid();

                await _mediator.Send(updateHospitalPoliclinicCommand);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalPoliclinicsController),
                    nameof(UpdateHospitalPoliclinicData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.DataUpdated,
                    TimeStamp = DateTime.UtcNow,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalPoliclinicsController),
                    nameof(UpdateHospitalPoliclinicData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHospitalPoliclinicData(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteHospitalPoliclinicCommand(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalPoliclinicsController),
                    nameof(DeleteHospitalPoliclinicData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.DataDelete,
                    TimeStamp = DateTime.UtcNow,
                });
            }
            catch (Exception ex)
            {

                _logger.LogError("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalPoliclinicsController),
                    nameof(DeleteHospitalPoliclinicData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }
    }
}

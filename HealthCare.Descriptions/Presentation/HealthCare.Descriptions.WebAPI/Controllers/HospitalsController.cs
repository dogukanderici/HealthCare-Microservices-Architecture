using HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.HospitalQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalResults;
using HealthCare.Descriptions.WebAPI.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Specialized;
using System.ComponentModel;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<HospitalsController> _logger;

        public HospitalsController(IMediator mediator, ILogger<HospitalsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetHospitalsData()
        {
            try
            {
                List<GetHospitalsQueryResult> result = await _mediator.Send(new GetHospitalsQuery());

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalsController),
                    nameof(GetHospitalsData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetHospitalsQueryResult>
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.Success,
                    TimeStamp = DateTime.UtcNow,
                    Datas = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalsController),
                    nameof(GetHospitalsData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHospitalByIdData(Guid id)
        {
            try
            {
                GetHospitalByIdQueryResult result = await _mediator.Send(new GetHospitalByIdQuery(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalsController),
                    nameof(GetHospitalByIdData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetHospitalByIdQueryResult>
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.Success,
                    TimeStamp = DateTime.UtcNow,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalsController),
                    nameof(GetHospitalByIdData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpGet("params/{param}")]
        public async Task<IActionResult> GetHospitalsByFilterData(string param)
        {
            try
            {
                NameValueCollection queryParameters = System.Web.HttpUtility.ParseQueryString(param);

                int? hospitalCode = int.TryParse(queryParameters["HospitalCode"], out int chekcHospitalCode) ? chekcHospitalCode : null;
                string hospitalName = queryParameters["HospitalName"];
                int? hospitalCity = int.TryParse(queryParameters["HospitalCity"], out int chekcHospitalCity) ? chekcHospitalCity : null;
                Guid? hosptialDistict = Guid.TryParse(queryParameters["HospitalDistrict"], out Guid checkHosptialDistict) ? checkHosptialDistict : null;
                bool? isAvailable = bool.TryParse(queryParameters["IsAvailable"], out bool chekcIsAvailable) ? chekcIsAvailable : null;

                GetHospitalsByFilterQuery queryParams = GetHospitalsByFilterQuery.Filter(hospitalCode, hospitalName, hospitalCity, hosptialDistict, isAvailable);

                List<GetHospitalsByFilterQueryResult> result = await _mediator.Send(queryParams);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalsController),
                    nameof(GetHospitalsByFilterData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetHospitalsByFilterQueryResult>
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.Success,
                    TimeStamp = DateTime.UtcNow,
                    Datas = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalsController),
                    nameof(GetHospitalsByFilterData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateHospitalData(CreateHospitalCommand createHospitalCommand)
        {
            try
            {
                createHospitalCommand.CreatedAt = DateTime.UtcNow;
                createHospitalCommand.CreatedBy = Guid.NewGuid();
                createHospitalCommand.UpdatedAt = DateTime.UtcNow;
                createHospitalCommand.UpdatedBy = Guid.NewGuid();

                await _mediator.Send(createHospitalCommand);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalsController),
                    nameof(CreateHospitalData),
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
                    nameof(HospitalsController),
                    nameof(CreateHospitalData),
                    ApiResponseMessageConstant.CallingSuccess);

                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHospitalData(UpdateHospitalCommand updateHospitalCommand)
        {
            try
            {
                if (updateHospitalCommand.CreatedAt == DateTimeOffset.MinValue && updateHospitalCommand.CreatedBy == Guid.Empty)
                {
                    GetHospitalByIdQueryResult result = await _mediator.Send(new GetHospitalByIdQuery(updateHospitalCommand.Id));

                    if (result != null)
                    {
                        updateHospitalCommand.CreatedAt = result.CreatedAt;
                        updateHospitalCommand.CreatedBy = result.CreatedBy;
                    }
                    else
                    {
                        updateHospitalCommand.CreatedAt = DateTime.UtcNow;
                        updateHospitalCommand.CreatedBy = Guid.NewGuid();
                    }
                }

                updateHospitalCommand.UpdatedAt = DateTime.UtcNow;
                updateHospitalCommand.UpdatedBy = Guid.NewGuid();

                await _mediator.Send(updateHospitalCommand);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalsController),
                    nameof(UpdateHospitalData),
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
                    nameof(HospitalsController),
                    nameof(UpdateHospitalData),
                    ApiResponseMessageConstant.CallingSuccess);

                throw;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHospitalData(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteHospitalCommand(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(HospitalsController),
                    nameof(DeleteHospitalData),
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
                    nameof(HospitalsController),
                    nameof(DeleteHospitalData),
                    ApiResponseMessageConstant.CallingSuccess);

                throw;
            }
        }
    }
}

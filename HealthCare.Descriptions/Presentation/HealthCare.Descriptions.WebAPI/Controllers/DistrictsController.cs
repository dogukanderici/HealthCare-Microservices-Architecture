using HealthCare.Descriptions.Application.Features.Mediator.Commands.DistrictCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.DistrictQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.DistrictResults;
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
    public class DistrictsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DistrictsController> _logger;

        public DistrictsController(IMediator mediator, ILogger<DistrictsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetDistrictsData()
        {
            try
            {
                List<GetDistrictsQueryResult> result = await _mediator.Send(new GetDistrictsQuery());

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(DistrictsController),
                    nameof(GetDistrictsData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetDistrictsQueryResult>
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
                    nameof(DistrictsController),
                    nameof(GetDistrictsData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDistrictByIdData(Guid id)
        {
            try
            {
                GetDistrictByIdQueryResult result = await _mediator.Send(new GetDistrictByIdQuery(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(DistrictsController),
                    nameof(GetDistrictByIdData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetDistrictByIdQueryResult>
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
                    nameof(DistrictsController),
                    nameof(GetDistrictByIdData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }
        [HttpGet("params/{param}")]
        public async Task<IActionResult> GetDistrictsByFilterData(string param)
        {
            try
            {
                NameValueCollection queryParameters = System.Web.HttpUtility.ParseQueryString(param);

                int? plate = int.TryParse(queryParameters["Plate"], out int checkPlate) ? checkPlate : null;
                string? districtName = queryParameters["DistrictName"];
                bool? isAvailable = bool.TryParse(queryParameters["IsAvailable"], out bool chekcIsAvailable) ? chekcIsAvailable : null;

                GetDistrictsByFilterQuery queryParams = GetDistrictsByFilterQuery.Filter(plate, districtName, isAvailable);

                List<GetDistrictsByFilterQueryResult> result = await _mediator.Send(queryParams);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(DistrictsController),
                    nameof(GetDistrictsByFilterData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetDistrictsByFilterQueryResult>
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
                    nameof(DistrictsController),
                    nameof(GetDistrictsByFilterData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDistrictData(CreateDistrictCommand createDistrictCommand)
        {
            try
            {
                createDistrictCommand.CreatedAt = DateTimeOffset.UtcNow;
                createDistrictCommand.CreatedBy = Guid.NewGuid();
                createDistrictCommand.UpdatedAt = DateTimeOffset.UtcNow;
                createDistrictCommand.UpdatedBy = Guid.NewGuid();

                await _mediator.Send(createDistrictCommand);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(DistrictsController),
                    nameof(CreateDistrictData),
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
                    nameof(DistrictsController),
                    nameof(CreateDistrictData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDistrictData(UpdateDistrictCommand updateDistrictCommand)
        {
            try
            {

                if (updateDistrictCommand.CreatedAt == DateTimeOffset.MinValue && updateDistrictCommand.CreatedBy == Guid.Empty)
                {
                    GetDistrictByIdQueryResult result = await _mediator.Send(new GetDistrictByIdQuery(updateDistrictCommand.Id));

                    updateDistrictCommand.CreatedAt = result.CreatedAt;
                    updateDistrictCommand.CreatedBy = result.CreatedBy;
                }

                updateDistrictCommand.UpdatedAt = DateTimeOffset.UtcNow;
                updateDistrictCommand.UpdatedBy = Guid.NewGuid();

                await _mediator.Send(updateDistrictCommand);

                _logger.LogInformation("Controller: {} Action: {Action} Message: {Message}",
                   nameof(DistrictsController),
                   nameof(UpdateDistrictData),
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
                _logger.LogError("Controller: {} Action: {Action} Message: {Message}",
                   nameof(DistrictsController),
                   nameof(UpdateDistrictData),
                   ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDistrictData(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteDistrictCommand(id));

                _logger.LogInformation("Controller: {} Action: {Action} Message: {Message}",
                    nameof(DistrictsController),
                    nameof(DeleteDistrictData),
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
                _logger.LogError("Controller: {} Action: {Action} Message: {Message}",
                    nameof(DistrictsController),
                    nameof(DeleteDistrictData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }
    }
}

using HealthCare.Descriptions.Application.Features.Mediator.Commands.PoliclinicCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.PoliclinicQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.PoliclinicResults;
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
    public class PoliclinicsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PoliclinicsController> _logger;

        public PoliclinicsController(IMediator mediator, ILogger<PoliclinicsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetPoliclinicsData()
        {
            try
            {
                List<GetPoliclinicsQueryResult> result = await _mediator.Send(new GetPoliclinicsQuery());

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(PoliclinicsController),
                    nameof(GetPoliclinicsData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetPoliclinicsQueryResult>
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
                    nameof(PoliclinicsController),
                    nameof(GetPoliclinicsData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPoliclinicByIdData(Guid id)
        {
            try
            {
                GetPoliclinicByIdQueryResult result = await _mediator.Send(new GetPoliclinicByIdQuery(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(PoliclinicsController),
                    nameof(GetPoliclinicByIdData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetPoliclinicByIdQueryResult>
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
                    nameof(PoliclinicsController),
                    nameof(GetPoliclinicByIdData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpGet("params/{parameters}")]
        public async Task<IActionResult> GetPoliclinicsByFilterData(string parameters)
        {
            try
            {
                NameValueCollection queryParameters = System.Web.HttpUtility.ParseQueryString(parameters);

                int? policlinicCode = int.TryParse(queryParameters["PoliclinicCode"], out int checkPoliclinicCode) ? checkPoliclinicCode : null;
                string policlinicName = queryParameters["PoliclinicName"];
                bool? isAvailable = bool.TryParse(queryParameters["IsAvailable"], out bool checkIsAvailable) ? checkIsAvailable : null;

                GetPoliclinicsByFilterQuery queryParams = GetPoliclinicsByFilterQuery.Filter(policlinicCode, policlinicName, isAvailable);

                List<GetPoliclinicsByFilterQueryResult> result = await _mediator.Send(queryParams);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(PoliclinicsController),
                    nameof(GetPoliclinicsByFilterData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetPoliclinicsByFilterQueryResult>
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
                    nameof(PoliclinicsController),
                    nameof(GetPoliclinicsByFilterData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePoliclinicData(CreatePoliclinicCommand createPoliclinicCommand)
        {
            try
            {
                createPoliclinicCommand.CreatedAt = DateTime.UtcNow;
                createPoliclinicCommand.CreatedBy = Guid.NewGuid();
                createPoliclinicCommand.UpdatedAt = DateTime.UtcNow;
                createPoliclinicCommand.UpdatedBy = Guid.NewGuid();

                await _mediator.Send(createPoliclinicCommand);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(PoliclinicsController),
                    nameof(CreatePoliclinicData),
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
                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(PoliclinicsController),
                    nameof(CreatePoliclinicData),
                    ApiResponseMessageConstant.CallingSuccess);

                throw;
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePoliclinicData(UpdatePoliclinicCommand updatePoliclinicCommand)
        {
            try
            {
                if (updatePoliclinicCommand.CreatedAt == DateTimeOffset.MinValue && updatePoliclinicCommand.CreatedBy == Guid.Empty)
                {
                    GetPoliclinicByIdQueryResult result = await _mediator.Send(new GetPoliclinicByIdQuery(updatePoliclinicCommand.Id));

                    if (result != null)
                    {
                        updatePoliclinicCommand.CreatedAt = result.CreatedAt;
                        updatePoliclinicCommand.CreatedBy = result.CreatedBy;
                    }
                    else
                    {
                        updatePoliclinicCommand.CreatedAt = DateTime.UtcNow;
                        updatePoliclinicCommand.CreatedBy = Guid.NewGuid();
                    }
                }

                updatePoliclinicCommand.UpdatedAt = DateTime.UtcNow;
                updatePoliclinicCommand.UpdatedBy = Guid.NewGuid();

                await _mediator.Send(updatePoliclinicCommand);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(PoliclinicsController),
                    nameof(UpdatePoliclinicData),
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
                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(PoliclinicsController),
                    nameof(UpdatePoliclinicData),
                    ApiResponseMessageConstant.CallingSuccess);

                throw;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePoliclinicData(Guid id)
        {
            try
            {
                await _mediator.Send(new DeletePoliclinicCommand(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(PoliclinicsController),
                    nameof(DeletePoliclinicData),
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

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(PoliclinicsController),
                    nameof(DeletePoliclinicData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }
    }
}
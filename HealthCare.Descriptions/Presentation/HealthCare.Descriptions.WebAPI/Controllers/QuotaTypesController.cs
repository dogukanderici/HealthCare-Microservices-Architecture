using HealthCare.Descriptions.Application.Features.Mediator.Commands.QuotaTypeCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.QuotaTypeQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.QuotaTypeResults;
using HealthCare.Descriptions.WebAPI.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Specialized;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotaTypesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<QuotaTypesController> _logger;

        public QuotaTypesController(IMediator mediator, ILogger<QuotaTypesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuotaTypesData()
        {
            try
            {
                List<GetQuotaTypesQueryResult> result = await _mediator.Send(new GetQuotaTypesQuery());

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(QuotaTypesController),
                    nameof(GetQuotaTypesData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetQuotaTypesQueryResult>
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
                    nameof(QuotaTypesController),
                    nameof(GetQuotaTypesData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuotaTypeByIdData(Guid id)
        {
            try
            {
                GetQuotaTypeByIdQueryResult result = await _mediator.Send(new GetQuotaTypeByIdQuery(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(QuotaTypesController),
                    nameof(GetQuotaTypeByIdData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetQuotaTypeByIdQueryResult>
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
                    nameof(QuotaTypesController),
                    nameof(GetQuotaTypeByIdData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpGet("params/{parameters}")]
        public async Task<IActionResult> GetQuotaTypesByFilterData(string parameters)
        {
            try
            {
                NameValueCollection queryParameters = System.Web.HttpUtility.ParseQueryString(parameters);

                string? quotaTypeNameStatus = queryParameters["QuotaTypeName"];

                bool? IsAvailable = bool.TryParse(queryParameters["IsAvailable"], out bool checkIsAvailable) ? checkIsAvailable : null;

                GetQuotaTypesByFilterQuery queryParameter = GetQuotaTypesByFilterQuery.Filter(quotaTypeNameStatus, IsAvailable);

                List<GetQuotaTypesByFilterQueryResult> result = await _mediator.Send(queryParameter);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(QuotaTypesController),
                    nameof(GetQuotaTypesByFilterData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetQuotaTypesByFilterQueryResult>
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
                    nameof(QuotaTypesController),
                    nameof(GetQuotaTypesByFilterData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuotaTypeData(CreateQuotaTypeCommand createQuotaTypeCommand)
        {
            try
            {
                createQuotaTypeCommand.CreatedAt = DateTimeOffset.UtcNow;
                createQuotaTypeCommand.CreatedBy = Guid.NewGuid();
                createQuotaTypeCommand.UpdatedAt = DateTimeOffset.UtcNow;
                createQuotaTypeCommand.UpdatedBy = Guid.NewGuid();

                await _mediator.Send(createQuotaTypeCommand);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(QuotaTypesController),
                    nameof(CreateQuotaTypeData),
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
                    nameof(QuotaTypesController),
                    nameof(CreateQuotaTypeData),
                    ApiResponseMessageConstant.CallingFail);

                throw;

            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuotaTypeData(UpdateQuotaTypeCommand updateQuotaTypeCommand)
        {
            try
            {
                if (updateQuotaTypeCommand.CreatedAt == DateTimeOffset.MinValue &&
                    updateQuotaTypeCommand.CreatedBy == Guid.Empty)
                {
                    GetQuotaTypeByIdQueryResult result = await _mediator.Send(new GetQuotaTypeByIdQuery(updateQuotaTypeCommand.Id));

                    if (result != null)
                    {
                        updateQuotaTypeCommand.CreatedAt = result.CreatedAt;
                        updateQuotaTypeCommand.CreatedBy = result.CreatedBy;
                    }
                    else
                    {
                        updateQuotaTypeCommand.CreatedAt = DateTimeOffset.UtcNow;
                        updateQuotaTypeCommand.CreatedBy = Guid.NewGuid();
                    }
                }

                updateQuotaTypeCommand.UpdatedAt = DateTimeOffset.UtcNow;
                updateQuotaTypeCommand.UpdatedBy = Guid.NewGuid();

                await _mediator.Send(updateQuotaTypeCommand);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(QuotaTypesController),
                    nameof(UpdateQuotaTypeData),
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
                    nameof(QuotaTypesController),
                    nameof(UpdateQuotaTypeData),
                    ApiResponseMessageConstant.CallingFail);

                throw;

            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteQuotaTypeData(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteQuotaTypeCommand(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(QuotaTypesController),
                    nameof(DeleteQuotaTypeData),
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
                    nameof(QuotaTypesController),
                    nameof(DeleteQuotaTypeData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }
    }
}

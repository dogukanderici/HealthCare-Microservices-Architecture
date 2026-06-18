using HealthCare.Descriptions.Application.Features.Mediator.Commands.ServiceCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.ServiceQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.ServiceResults;
using HealthCare.Descriptions.WebAPI.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ServiceTypesController> _logger;

        public ServiceTypesController(IMediator mediator, ILogger<ServiceTypesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetServiceTypesData()
        {
            try
            {
                List<GetServiceTypesQueryResult> result = await _mediator.Send(new GetServiceTypesQuery());

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(ServiceTypesController),
                    nameof(GetServiceTypesData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetServiceTypesQueryResult>
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
                    nameof(ServiceTypesController),
                    nameof(GetServiceTypesData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceTypeByIdData(Guid id)
        {
            try
            {
                GetServiceTypeByIdQueryResult result = await _mediator.Send(new GetServiceTypeByIdQuery(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(ServiceTypesController),
                    nameof(GetServiceTypeByIdData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetServiceTypeByIdQueryResult>
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
                    nameof(ServiceTypesController),
                    nameof(GetServiceTypeByIdData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpGet("params/{nameValueCollection}")]
        public async Task<IActionResult> GetServiceTypesByFilterData(string nameValueCollection)
        {
            try
            {
                NameValueCollection queryParameters = System.Web.HttpUtility.ParseQueryString(nameValueCollection);

                string serviceCode = queryParameters.Get("ServiceCode");
                string serviceName = queryParameters.Get("ServiceName");
                bool? isAvailable = bool.TryParse(queryParameters.Get("IsAvailable"), out bool chekcIsAvailable) ? chekcIsAvailable : null;

                GetServiceTypesByFilterQuery fullFilter = GetServiceTypesByFilterQuery.FullFilter(serviceCode, serviceName, isAvailable);

                List<GetServiceTypesByFilterQueryResult> result = await _mediator.Send(fullFilter);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(ServiceTypesController),
                    nameof(GetServiceTypesByFilterData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse<GetServiceTypesByFilterQueryResult>
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
                    nameof(ServiceTypesController),
                    nameof(GetServiceTypesByFilterData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateServiceTypeData(CreateServiceTypeCommand createServiceTypeCommand)
        {
            try
            {
                createServiceTypeCommand.CreatedAt = DateTimeOffset.UtcNow;
                createServiceTypeCommand.CreatedBy = Guid.NewGuid();
                createServiceTypeCommand.UpdatedAt = DateTimeOffset.UtcNow;
                createServiceTypeCommand.UpdatedBy = Guid.NewGuid();

                await _mediator.Send(createServiceTypeCommand);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(ServiceTypesController),
                    nameof(CreateServiceTypeData),
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
                    nameof(ServiceTypesController),
                    nameof(CreateServiceTypeData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateServiceTypeData(UpdateServiceTypeCommand updateServiceTypeCommand)
        {
            try
            {

                if (updateServiceTypeCommand.CreatedAt == DateTimeOffset.MinValue && updateServiceTypeCommand.CreatedBy == Guid.Empty)
                {
                    GetServiceTypeByIdQueryResult result = await _mediator.Send(new GetServiceTypeByIdQuery(updateServiceTypeCommand.Id));

                    updateServiceTypeCommand.CreatedAt = result.CreatedAt;
                    updateServiceTypeCommand.CreatedBy = result.CreatedBy;
                }

                updateServiceTypeCommand.UpdatedAt = DateTimeOffset.UtcNow;
                updateServiceTypeCommand.UpdatedBy = Guid.NewGuid();

                await _mediator.Send(updateServiceTypeCommand);

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(ServiceTypesController),
                    nameof(UpdateServiceTypeData),
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
                    nameof(ServiceTypesController),
                    nameof(UpdateServiceTypeData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteServiceTypeData(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteServiceTypeCommand(id));

                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(ServiceTypesController),
                    nameof(DeleteServiceTypeData),
                    ApiResponseMessageConstant.CallingSuccess);

                return Ok(new GenericApiResponse
                {
                    StatusCode = 200,
                    Message = ApiResponseMessageConstant.Success,
                    TimeStamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Controller: {Controller} Action: {Action} Message: {Message}",
                    nameof(ServiceTypesController),
                    nameof(DeleteServiceTypeData),
                    ApiResponseMessageConstant.CallingFail);

                throw;
            }
        }
    }
}

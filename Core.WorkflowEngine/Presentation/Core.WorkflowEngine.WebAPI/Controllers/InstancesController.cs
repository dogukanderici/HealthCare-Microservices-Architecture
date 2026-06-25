using Core.WorkflowEngine.Application.Features.Mediator.Commands.InstanceCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.InstanceQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Domain.Entities;
using Core.WorkflowEngine.WebAPI.Constants;
using Core.WorkflowEngine.WebAPI.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Core.WorkflowEngine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstancesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<InstancesController> _logger;

        public InstancesController(IMediator mediator, ILogger<InstancesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetInstances()
        {
            try
            {
                List<GetInstancesQueryResult> result = await _mediator.Send(new GetInstancesQuery());

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(InstancesController),
                    nameof(GetInstances),
                    LogConstants.SuccessMessage.CallingSuccess);

                return Ok(GenericAPIResponse<List<GetInstancesQueryResult>>.SuccessAPIResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                                    nameof(InstancesController),
                                    nameof(GetInstances),
                                    LogConstants.ErrorMessage.CallingFail);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstanceById(Guid id)
        {
            try
            {
                GetInstanceByIdQueryResult result = await _mediator.Send(new GetInstanceByIdQuery(id));

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(InstancesController),
                    nameof(GetInstanceById),
                    LogConstants.SuccessMessage.CallingSuccess);

                return Ok(GenericAPIResponse<GetInstanceByIdQueryResult>.SuccessAPIResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                                    nameof(InstancesController),
                                    nameof(GetInstanceById),
                                    LogConstants.ErrorMessage.CallingFail);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpPost("filtered")]
        public async Task<IActionResult> GetInstancesByFilter(GetInstanceByFilterQuery query)
        {
            try
            {
                GetInstanceByFilterQuery filter = GetInstanceByFilterQuery.Filter(query.Number, query.InitiatorWorkItemId, query.Status);

                List<GetInstancesByFilterQueryResult> result = await _mediator.Send(filter);

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(InstancesController),
                    nameof(GetInstancesByFilter),
                    LogConstants.SuccessMessage.CallingSuccess);

                return Ok(GenericAPIResponse<List<GetInstancesByFilterQueryResult>>.SuccessAPIResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(InstancesController),
                    nameof(GetInstancesByFilter),
                    LogConstants.ErrorMessage.CallingFail);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateInstance(CreateInstanceCommand createInstanceCommand)
        {
            try
            {
                InternalCommandResponse<Guid> result = await _mediator.Send(createInstanceCommand);

                if (!result.IsSuccess)
                {
                    _logger.LogError(LogConstants.LogMessageTemplate,
                                    nameof(InstancesController),
                                    nameof(GetInstances),
                                    LogConstants.ErrorMessage.AddingDataFailed);

                    return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
                }

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(InstancesController),
                    nameof(GetInstances),
                    LogConstants.SuccessMessage.CallingSuccess);


                return Ok(GenericAPIResponse<Guid>.SuccessAPIResponse(result.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                                    nameof(InstancesController),
                                    nameof(GetInstances),
                                    $"{LogConstants.ErrorMessage.AddingDataFailed} - {ex}");

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInstance(UpdateInstanceCommand updateInstanceCommand)
        {
            try
            {
                InternalCommandResponse<DateTimeOffset> result = await _mediator.Send(updateInstanceCommand);

                if (!result.IsSuccess)
                {
                    _logger.LogError(LogConstants.LogMessageTemplate,
                        nameof(InstancesController),
                        nameof(UpdateInstance),
                        LogConstants.ErrorMessage.UpdatingDataFailed);

                    return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
                }

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(InstancesController),
                    nameof(UpdateInstance),
                    LogConstants.SuccessMessage.UpdatingDataSuccessed);

                return Ok(GenericAPIResponse<DateTimeOffset>.SuccessAPIResponse(result.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                                                    nameof(InstancesController),
                                                    nameof(UpdateInstance),
                                                    $"{LogConstants.ErrorMessage.UpdatingDataFailed} - {ex}");

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteInstance(DeleteInstanceCommand deleteInstanceCommand)
        {
            try
            {
                InternalCommandResponse<bool> result = await _mediator.Send(new DeleteInstanceCommand(deleteInstanceCommand.Id));

                if (!result.IsSuccess)
                {
                    _logger.LogError(LogConstants.LogMessageTemplate,
                        nameof(InstancesController),
                        nameof(UpdateInstance),
                        LogConstants.ErrorMessage.DeletingDataFailed);

                    return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
                }

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(InstancesController),
                    nameof(UpdateInstance),
                    LogConstants.SuccessMessage.UpdatingDataSuccessed);

                return Ok(GenericAPIResponse<bool>.SuccessAPIResponse(true));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                                                   nameof(InstancesController),
                                                   nameof(DeleteInstance),
                                                   $"{LogConstants.ErrorMessage.DeletingDataFailed} - {ex}");

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }
    }
}
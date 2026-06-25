using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskActionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskActionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskActionResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.WebAPI.Constants;
using Core.WorkflowEngine.WebAPI.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.WorkflowEngine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskActionsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TaskActionsController> _logger;

        public TaskActionsController(IMediator mediator, ILogger<TaskActionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskActions()
        {
            try
            {
                List<GetProcessTaskActionsQueryResult> result = await _mediator.Send(new GetProcessTaskActionsQuery());

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessTasksController),
                    nameof(GetTaskActions),
                    LogConstants.SuccessMessage.CallingSuccess);

                return Ok(GenericAPIResponse<List<GetProcessTaskActionsQueryResult>>.SuccessAPIResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(ProcessTasksController),
                    nameof(GetTaskActions),
                    ex);

                return Ok(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskActionById(Guid id)
        {
            try
            {
                GetProcessTaskActionByIdQueryResult result = await _mediator.Send(new GetProcessTaskActionByIdQuery(id));

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessTasksController),
                    nameof(GetTaskActionById),
                    LogConstants.SuccessMessage.CallingSuccess);

                return Ok(GenericAPIResponse<GetProcessTaskActionByIdQueryResult>.SuccessAPIResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(ProcessTasksController),
                    nameof(GetTaskActionById),
                    ex);

                return Ok(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpPost("filtered")]
        public async Task<IActionResult> GetTaskActionsByFilter(GetProcessTaskActionsByFilterQuery query)
        {
            try
            {
                GetProcessTaskActionsByFilterQuery filter = GetProcessTaskActionsByFilterQuery.Filter(
                    query.ProcessTaskId,
                    query.ActionId,
                    query.ActionName,
                    query.ActionType,
                    query.IsActive
                    );

                List<GetProcessTaskActionsByFilterQueryResult> result = await _mediator.Send(filter);

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessTasksController),
                    nameof(GetTaskActionsByFilter),
                    LogConstants.SuccessMessage.CallingSuccess);

                return Ok(GenericAPIResponse<List<GetProcessTaskActionsByFilterQueryResult>>.SuccessAPIResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(ProcessTasksController),
                    nameof(GetTaskActionsByFilter),
                    ex);

                return Ok(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskAction(CreateProcessTaskActionCommand createProcessTaskActionCommand)
        {
            try
            {
                InternalCommandResponse<Guid> result = await _mediator.Send(createProcessTaskActionCommand);

                if (result.IsSuccess)
                {
                    _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(ProcessTasksController),
                        nameof(CreateTaskAction),
                        LogConstants.SuccessMessage.AddingDataSuccessed);

                    return Ok(GenericAPIResponse<Guid>.SuccessAPIResponse(result.Data));
                }

                _logger.LogError(LogConstants.LogMessageTemplate,
                       nameof(ProcessTasksController),
                       nameof(CreateTaskAction),
                       LogConstants.ErrorMessage.AddingDataFailed);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());

            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                       nameof(ProcessTasksController),
                       nameof(CreateTaskAction),
                       ex);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTaskAction(UpdateProcessTaskActionCommand updateProcessTaskActionCommand)
        {
            try
            {
                InternalCommandResponse<DateTimeOffset> result = await _mediator.Send(updateProcessTaskActionCommand);

                if (result.IsSuccess)
                {
                    _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(ProcessTasksController),
                        nameof(UpdateTaskAction),
                        LogConstants.SuccessMessage.UpdatingDataSuccessed);

                    return Ok(GenericAPIResponse<DateTimeOffset>.SuccessAPIResponse(result.Data));
                }

                _logger.LogError(LogConstants.LogMessageTemplate,
                       nameof(ProcessTasksController),
                       nameof(UpdateTaskAction),
                       LogConstants.ErrorMessage.UpdatingDataFailed);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());

            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                       nameof(ProcessTasksController),
                       nameof(UpdateTaskAction),
                       ex);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskAction(Guid id)
        {
            try
            {
                InternalCommandResponse<bool> result = await _mediator.Send(new DeleteProcessTaskActionCommand(id));

                if (result.IsSuccess)
                {
                    _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(ProcessTasksController),
                        nameof(DeleteTaskAction),
                        LogConstants.SuccessMessage.DeletingDataSuccessed);

                    return Ok(GenericAPIResponse<bool>.SuccessAPIResponse(result.Data));
                }

                _logger.LogError(LogConstants.LogMessageTemplate,
                       nameof(ProcessTasksController),
                       nameof(DeleteTaskAction),
                       LogConstants.ErrorMessage.DeletingDataFailed);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());

            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                       nameof(ProcessTasksController),
                       nameof(DeleteTaskAction),
                       ex);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }
    }
}
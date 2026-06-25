using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskTransitionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskTransitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults;
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
    public class TaskTransitionsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TaskTransitionsController> _logger;

        public TaskTransitionsController(IMediator mediator, ILogger<TaskTransitionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskTransitions()
        {
            try
            {
                List<GetProcessTaskTransitionsQueryResult> result = await _mediator.Send(new GetProcessTaskTransitionsQuery());

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessTasksController),
                    nameof(GetTaskTransitions),
                    LogConstants.SuccessMessage.CallingSuccess);

                return Ok(GenericAPIResponse<List<GetProcessTaskTransitionsQueryResult>>.SuccessAPIResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(ProcessTasksController),
                    nameof(GetTaskTransitions),
                    ex);

                return Ok(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskTransitionById(Guid id)
        {
            try
            {
                GetProcessTaskTransitionByIdQueryResult result = await _mediator.Send(new GetProcessTaskTransitionByIdQuery(id));

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessTasksController),
                    nameof(GetTaskTransitionById),
                    LogConstants.SuccessMessage.CallingSuccess);

                return Ok(GenericAPIResponse<GetProcessTaskTransitionByIdQueryResult>.SuccessAPIResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(ProcessTasksController),
                    nameof(GetTaskTransitionById),
                    ex);

                return Ok(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpPost("filtered")]
        public async Task<IActionResult> GetTaskTransitionsByFilter(GetProcessTaskTransitionsByFilterQuery query)
        {
            try
            {
                GetProcessTaskTransitionsByFilterQuery filter = GetProcessTaskTransitionsByFilterQuery.Filter(
                    query.ProcessTaskId,
                    query.NextTaskId,
                    query.ActionId,
                    query.IsActive
                    );

                List<GetProcessTaskTransitionsByFilterQueryResult> result = await _mediator.Send(filter);

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessTasksController),
                    nameof(GetTaskTransitionsByFilter),
                    LogConstants.SuccessMessage.CallingSuccess);

                return Ok(GenericAPIResponse<List<GetProcessTaskTransitionsByFilterQueryResult>>.SuccessAPIResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(ProcessTasksController),
                    nameof(GetTaskTransitionsByFilter),
                    ex);

                return Ok(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskTransition(CreateProcessTaskTransitionCommand createProcessTaskTransitionCommand)
        {
            try
            {
                InternalCommandResponse<Guid> result = await _mediator.Send(createProcessTaskTransitionCommand);

                if (result.IsSuccess)
                {
                    _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(ProcessTasksController),
                        nameof(CreateTaskTransition),
                        LogConstants.SuccessMessage.AddingDataSuccessed);

                    return Ok(GenericAPIResponse<Guid>.SuccessAPIResponse(result.Data));
                }

                _logger.LogError(LogConstants.LogMessageTemplate,
                       nameof(ProcessTasksController),
                       nameof(CreateTaskTransition),
                       LogConstants.ErrorMessage.AddingDataFailed);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());

            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                       nameof(ProcessTasksController),
                       nameof(CreateTaskTransition),
                       ex);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTaskTransition(UpdateProcessTaskTransitionCommand updateProcessTaskTransitionCommand)
        {
            try
            {
                InternalCommandResponse<DateTimeOffset> result = await _mediator.Send(updateProcessTaskTransitionCommand);

                if (result.IsSuccess)
                {
                    _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(ProcessTasksController),
                        nameof(UpdateTaskTransition),
                        LogConstants.SuccessMessage.UpdatingDataSuccessed);

                    return Ok(GenericAPIResponse<DateTimeOffset>.SuccessAPIResponse(result.Data));
                }

                _logger.LogError(LogConstants.LogMessageTemplate,
                       nameof(ProcessTasksController),
                       nameof(UpdateTaskTransition),
                       LogConstants.ErrorMessage.UpdatingDataFailed);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());

            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                       nameof(ProcessTasksController),
                       nameof(UpdateTaskTransition),
                       ex);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskTransition(Guid id)
        {
            try
            {
                InternalCommandResponse<bool> result = await _mediator.Send(new DeleteProcessTaskTransitionCommand(id));

                if (result.IsSuccess)
                {
                    _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(ProcessTasksController),
                        nameof(DeleteTaskTransition),
                        LogConstants.SuccessMessage.DeletingDataSuccessed);

                    return Ok(GenericAPIResponse<bool>.SuccessAPIResponse(result.Data));
                }

                _logger.LogError(LogConstants.LogMessageTemplate,
                       nameof(ProcessTasksController),
                       nameof(DeleteTaskTransition),
                       LogConstants.ErrorMessage.DeletingDataFailed);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());

            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                       nameof(ProcessTasksController),
                       nameof(DeleteTaskTransition),
                       ex);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }
    }
}
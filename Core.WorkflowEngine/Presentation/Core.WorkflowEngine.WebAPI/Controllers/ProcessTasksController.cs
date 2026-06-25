using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.WebAPI.Constants;
using Core.WorkflowEngine.WebAPI.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Core.WorkflowEngine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessTasksController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProcessTasksController> _logger;

        public ProcessTasksController(IMediator mediator, ILogger<ProcessTasksController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{processId}")]
        public async Task<IActionResult> GetTasks(Guid processId)
        {
            try
            {
                List<GetProcessTasksQueryResult> result = await _mediator.Send(new GetProcessTasksQuery(processId));

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessTasksController),
                    nameof(GetTasks),
                    LogConstants.SuccessMessage.CallingSuccess);

                return Ok(GenericAPIResponse<List<GetProcessTasksQueryResult>>.SuccessAPIResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(ProcessTasksController),
                    nameof(GetTasks),
                    ex);

                return Ok(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            try
            {
                GetProcessTaskByIdQueryResult result = await _mediator.Send(new GetProcessTaskByIdQuery(id));

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessTasksController),
                    nameof(GetTaskById),
                    LogConstants.SuccessMessage.CallingSuccess);

                return Ok(GenericAPIResponse<GetProcessTaskByIdQueryResult>.SuccessAPIResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(ProcessTasksController),
                    nameof(GetTaskById),
                    ex);

                return Ok(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpPost("filtered")]
        public async Task<IActionResult> GetTasksByFilter(GetProcessTasksByFilterQuery query)
        {
            try
            {
                GetProcessTasksByFilterQuery filter = GetProcessTasksByFilterQuery.Filter(
                    query.ProcessId,
                    query.StepName,
                    query.IsStartStep,
                    query.IsActive
                    );

                List<GetProcessTasksByFilterQueryResult> result = await _mediator.Send(filter);

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessTasksController),
                    nameof(GetTasksByFilter),
                    LogConstants.SuccessMessage.CallingSuccess);

                return Ok(GenericAPIResponse<List<GetProcessTasksByFilterQueryResult>>.SuccessAPIResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(ProcessTasksController),
                    nameof(GetTasksByFilter),
                    ex);

                return Ok(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateProcessTaskCommand createProcessTaskCommand)
        {
            try
            {
                InternalCommandResponse<Guid> result = await _mediator.Send(createProcessTaskCommand);

                if (result.IsSuccess)
                {
                    _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(ProcessTasksController),
                        nameof(CreateTask),
                        LogConstants.SuccessMessage.AddingDataSuccessed);

                    return Ok(GenericAPIResponse<Guid>.SuccessAPIResponse(result.Data));
                }

                _logger.LogError(LogConstants.LogMessageTemplate,
                       nameof(ProcessTasksController),
                       nameof(CreateTask),
                       LogConstants.ErrorMessage.AddingDataFailed);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());

            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                       nameof(ProcessTasksController),
                       nameof(CreateTask),
                       ex);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask(UpdateProcessTaskCommand updateProcessTaskCommand)
        {
            try
            {
                InternalCommandResponse<DateTimeOffset> result = await _mediator.Send(updateProcessTaskCommand);

                if (result.IsSuccess)
                {
                    _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(ProcessTasksController),
                        nameof(UpdateTask),
                        LogConstants.SuccessMessage.UpdatingDataSuccessed);

                    return Ok(GenericAPIResponse<DateTimeOffset>.SuccessAPIResponse(result.Data));
                }

                _logger.LogError(LogConstants.LogMessageTemplate,
                       nameof(ProcessTasksController),
                       nameof(UpdateTask),
                       LogConstants.ErrorMessage.UpdatingDataFailed);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());

            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                       nameof(ProcessTasksController),
                       nameof(UpdateTask),
                       ex);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            try
            {
                InternalCommandResponse<bool> result = await _mediator.Send(new DeleteProcessTaskCommand(id));

                if (result.IsSuccess)
                {
                    _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(ProcessTasksController),
                        nameof(DeleteTask),
                        LogConstants.SuccessMessage.DeletingDataSuccessed);

                    return Ok(GenericAPIResponse<bool>.SuccessAPIResponse(result.Data));
                }

                _logger.LogError(LogConstants.LogMessageTemplate,
                       nameof(ProcessTasksController),
                       nameof(DeleteTask),
                       LogConstants.ErrorMessage.DeletingDataFailed);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());

            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                       nameof(ProcessTasksController),
                       nameof(DeleteTask),
                       ex);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }
    }
}
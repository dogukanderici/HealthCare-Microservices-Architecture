using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessDefinitionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.WebAPI.Constants;
using Core.WorkflowEngine.WebAPI.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Core.WorkflowEngine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProcessesController> _logger;

        public ProcessesController(IMediator mediator, ILogger<ProcessesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProcesses()
        {
            try
            {
                List<GetProcessDefinitionsQueryResult> result = await _mediator.Send(new GetProcessDefinitionsQuery());

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessesController),
                    nameof(GetProcesses),
                    LogConstants.SuccessMessage.CallingSuccess);

                return Ok(GenericAPIResponse<List<GetProcessDefinitionsQueryResult>>.SuccessAPIResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                                    nameof(ProcessesController),
                                    nameof(GetProcesses),
                                    LogConstants.ErrorMessage.CallingFail);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProcessById(Guid id)
        {
            try
            {
                GetProcessDefinitionByIdQueryResult result = await _mediator.Send(new GetProcessDefinitionByIdQuery(id));

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessesController),
                    nameof(GetProcessById),
                    LogConstants.SuccessMessage.CallingSuccess);

                return Ok(GenericAPIResponse<GetProcessDefinitionByIdQueryResult>.SuccessAPIResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                                    nameof(ProcessesController),
                                    nameof(GetProcessById),
                                    LogConstants.ErrorMessage.CallingFail);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpPost("filtered")]
        public async Task<IActionResult> GetWorkItemsByFilter(GetWorkItemsByFilterQuery query)
        {
            try
            {
                GetWorkItemsByFilterQuery filter = GetWorkItemsByFilterQuery.Filter(
                    query.InstanceId,
                    query.AssignedUserId,
                    query.Status,
                    query.CreatedAt,
                    query.CreatedBy
                    );

                List<GetWorkItemsByFilterQueryResult> result = await _mediator.Send(filter);

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessesController),
                    nameof(GetWorkItemsByFilter),
                    LogConstants.SuccessMessage.CallingSuccess);

                return Ok(GenericAPIResponse<List<GetWorkItemsByFilterQueryResult>>.SuccessAPIResponse(result));

            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                                    nameof(ProcessesController),
                                    nameof(GetWorkItemsByFilter),
                                    LogConstants.ErrorMessage.CallingFail);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());

            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProcess(CreateProcessDefinitionCommand createProcessDefinitionCommand)
        {
            try
            {
                InternalCommandResponse<Guid> result = await _mediator.Send(createProcessDefinitionCommand);

                if (!result.IsSuccess)
                {
                    _logger.LogError(LogConstants.LogMessageTemplate,
                                    nameof(ProcessesController),
                                    nameof(CreateProcess),
                                    LogConstants.ErrorMessage.AddingDataFailed);

                    return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
                }

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessesController),
                    nameof(CreateProcess),
                    LogConstants.SuccessMessage.CallingSuccess);


                return Ok(GenericAPIResponse<Guid>.SuccessAPIResponse(result.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                                    nameof(ProcessesController),
                                    nameof(CreateProcess),
                                    $"{LogConstants.ErrorMessage.AddingDataFailed} - {ex}");

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProcess(UpdateProcessDefinitionCommand updateProcessDefinitionCommand)
        {
            try
            {
                InternalCommandResponse<DateTimeOffset> result = await _mediator.Send(updateProcessDefinitionCommand);

                if (!result.IsSuccess)
                {
                    _logger.LogError(LogConstants.LogMessageTemplate,
                        nameof(ProcessesController),
                        nameof(UpdateProcess),
                        LogConstants.ErrorMessage.UpdatingDataFailed);

                    return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
                }

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessesController),
                    nameof(UpdateProcess),
                    LogConstants.SuccessMessage.UpdatingDataSuccessed);

                return Ok(GenericAPIResponse<DateTimeOffset>.SuccessAPIResponse(result.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(ProcessesController),
                    nameof(UpdateProcess),
                    $"{LogConstants.ErrorMessage.UpdatingDataFailed} - {ex}");

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProcess(DeleteProcessDefinitionCommand deleteProcessDefinitionCommand)
        {
            try
            {
                InternalCommandResponse<bool> result = await _mediator.Send(new DeleteProcessDefinitionCommand(deleteProcessDefinitionCommand.Id));

                if (!result.IsSuccess)
                {
                    _logger.LogError(LogConstants.LogMessageTemplate,
                        nameof(ProcessesController),
                        nameof(DeleteProcess),
                        LogConstants.ErrorMessage.DeletingDataFailed);

                    return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
                }

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessesController),
                    nameof(DeleteProcess),
                    LogConstants.SuccessMessage.UpdatingDataSuccessed);

                return Ok(GenericAPIResponse<bool>.SuccessAPIResponse(true));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(ProcessesController),
                    nameof(DeleteProcess),
                    $"{LogConstants.ErrorMessage.DeletingDataFailed} - {ex}");

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }
    }
}
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskQueries;
using Core.WorkflowEngine.WebAPI.Helpers.ControllerResponseHelpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Core.WorkflowEngine.WebAPI.Constants.LogConstants;

namespace Core.WorkflowEngine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessTasksController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IControllerReponseHelper<ProcessTasksController> _controllerReponseHelper;

        public ProcessTasksController(IMediator mediator, IControllerReponseHelper<ProcessTasksController> controllerReponseHelper)
        {
            _mediator = mediator;
            _controllerReponseHelper = controllerReponseHelper;
        }

        [HttpGet("process/{processId}")]
        public async Task<IActionResult> GetTasks(Guid processId)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(new GetProcessTasksQuery(processId)),
                nameof(GetTasks),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(new GetProcessTaskByIdQuery(id)),
                nameof(GetTaskById),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpPost("filtered")]
        public async Task<IActionResult> GetTasksByFilter(GetProcessTasksByFilterQuery query)
        {
            GetProcessTasksByFilterQuery filter = GetProcessTasksByFilterQuery.Filter(
                query.ProcessId,
                query.StepName,
                query.IsStartStep,
                query.IsActive
                );

            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(filter),
                nameof(GetTasksByFilter),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateProcessTaskCommand createProcessTaskCommand)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(createProcessTaskCommand),
                nameof(CreateTask),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask(UpdateProcessTaskCommand updateProcessTaskCommand)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(updateProcessTaskCommand),
                nameof(UpdateTask),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(new DeleteProcessTaskCommand(id)),
                nameof(DeleteTask),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }
    }
}
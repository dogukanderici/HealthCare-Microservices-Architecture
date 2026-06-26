using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskActionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskActionQueries;
using Core.WorkflowEngine.WebAPI.Helpers.ControllerResponseHelpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Core.WorkflowEngine.WebAPI.Constants.LogConstants;

namespace Core.WorkflowEngine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskActionsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IControllerReponseHelper<TaskActionsController> _controllerReponseHelper;

        public TaskActionsController(IMediator mediator, IControllerReponseHelper<TaskActionsController> controllerReponseHelper)
        {
            _mediator = mediator;
            _controllerReponseHelper = controllerReponseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskActions()
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(new GetProcessTaskActionsQuery()),
                nameof(GetTaskActions),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskActionById(Guid id)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(new GetProcessTaskActionByIdQuery(id)),
                nameof(GetTaskActionById),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpPost("filtered")]
        public async Task<IActionResult> GetTaskActionsByFilter(GetProcessTaskActionsByFilterQuery query)
        {
            GetProcessTaskActionsByFilterQuery filter = GetProcessTaskActionsByFilterQuery.Filter(
                    query.ProcessTaskId,
                    query.ActionId,
                    query.ActionName,
                    query.ActionType,
                    query.IsActive
                    );

            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(filter),
                nameof(GetTaskActionsByFilter),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskAction(CreateProcessTaskActionCommand createProcessTaskActionCommand)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(createProcessTaskActionCommand),
                nameof(CreateTaskAction),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTaskAction(UpdateProcessTaskActionCommand updateProcessTaskActionCommand)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(updateProcessTaskActionCommand),
                nameof(UpdateTaskAction),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskAction(Guid id)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(new DeleteProcessTaskActionCommand(id)),
                nameof(DeleteTaskAction),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }
    }
}
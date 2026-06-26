using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskTransitionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskTransitionQueries;
using Core.WorkflowEngine.WebAPI.Helpers.ControllerResponseHelpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Core.WorkflowEngine.WebAPI.Constants.LogConstants;

namespace Core.WorkflowEngine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskTransitionsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IControllerReponseHelper<TaskTransitionsController> _controllerReponseHelper;

        public TaskTransitionsController(IMediator mediator, IControllerReponseHelper<TaskTransitionsController> controllerReponseHelper)
        {
            _mediator = mediator;
            _controllerReponseHelper = controllerReponseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskTransitions()
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(new GetProcessTaskTransitionsQuery()),
                nameof(GetTaskTransitions),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskTransitionById(Guid id)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(new GetProcessTaskTransitionByIdQuery(id)),
                nameof(GetTaskTransitionById),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpPost("filtered")]
        public async Task<IActionResult> GetTaskTransitionsByFilter(GetProcessTaskTransitionsByFilterQuery query)
        {
            GetProcessTaskTransitionsByFilterQuery filter = GetProcessTaskTransitionsByFilterQuery.Filter(
                    query.ProcessTaskId,
                    query.NextTaskId,
                    query.ActionId,
                    query.IsActive
                    );

            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(filter),
                nameof(GetTaskTransitionsByFilter),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskTransition(CreateProcessTaskTransitionCommand createProcessTaskTransitionCommand)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(createProcessTaskTransitionCommand),
                nameof(CreateTaskTransition),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTaskTransition(UpdateProcessTaskTransitionCommand updateProcessTaskTransitionCommand)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(updateProcessTaskTransitionCommand),
                nameof(UpdateTaskTransition),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskTransition(Guid id)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(new DeleteProcessTaskTransitionCommand(id)),
                nameof(DeleteTaskTransition),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }
    }
}
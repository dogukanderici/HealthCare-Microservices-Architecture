using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkflowExecutionCommands;
using Core.WorkflowEngine.WebAPI.Helpers.ControllerResponseHelpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using static Core.WorkflowEngine.WebAPI.Constants.LogConstants;

namespace Core.WorkflowEngine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowExecutionsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IControllerReponseHelper<WorkflowExecutionsController> _controllerResponseHelper;
        private readonly ILogger<WorkflowExecutionsController> _logger;

        public WorkflowExecutionsController(IMediator mediator, IControllerReponseHelper<WorkflowExecutionsController> controllerResponseHelper, ILogger<WorkflowExecutionsController> logger)
        {
            _mediator = mediator;
            _controllerResponseHelper = controllerResponseHelper;
            _logger = logger;
        }

        [HttpPost("InitiateInstance")]
        public Task<IActionResult> CreateNewInstance(CreateInstanceExecutionCommand createInstanceExecutionCommand)
        {
            return _controllerResponseHelper.ExecuteAsync(
                () => _mediator.Send(createInstanceExecutionCommand),
                nameof(CreateNewInstance),
                SuccessMessage.AddingDataSuccessed,
                ErrorMessage.CallingFail
                );
        }

    }
}

using Core.WorkflowEngine.Application.Features.Mediator.Commands.InstanceCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.InstanceQueries;
using Core.WorkflowEngine.WebAPI.Helpers.ControllerResponseHelpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using static Core.WorkflowEngine.WebAPI.Constants.LogConstants;

namespace Core.WorkflowEngine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstancesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IControllerReponseHelper<InstancesController> _controllerReponseHelper;

        public InstancesController(IMediator mediator, IControllerReponseHelper<InstancesController> controllerReponseHelper)
        {
            _mediator = mediator;
            _controllerReponseHelper = controllerReponseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetInstances()
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(new GetInstancesQuery()),
                nameof(GetInstances),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstanceById(Guid id)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(new GetInstanceByIdQuery(id)),
                nameof(GetInstanceById),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpPost("filtered")]
        public async Task<IActionResult> GetInstancesByFilter(GetInstanceByFilterQuery query)
        {
            GetInstanceByFilterQuery filter = GetInstanceByFilterQuery.Filter(query.Number, query.InitiatorWorkItemId, query.Status);

            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(filter),
                nameof(GetInstancesByFilter),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpPost]
        public async Task<IActionResult> CreateInstance(CreateInstanceCommand createInstanceCommand)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(createInstanceCommand),
                nameof(CreateInstance),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInstance(UpdateInstanceCommand updateInstanceCommand)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(updateInstanceCommand),
                nameof(UpdateInstance),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteInstance(DeleteInstanceCommand deleteInstanceCommand)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(new DeleteInstanceCommand(deleteInstanceCommand.Id)),
                nameof(DeleteInstance),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }
    }
}
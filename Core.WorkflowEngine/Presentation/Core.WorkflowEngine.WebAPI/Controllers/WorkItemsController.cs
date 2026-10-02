using Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries;
using Core.WorkflowEngine.WebAPI.Helpers.ControllerResponseHelpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Core.WorkflowEngine.WebAPI.Constants.LogConstants;

namespace Core.WorkflowEngine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkItemsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IControllerReponseHelper<WorkItemsController> _controllerReponseHelper;

        public WorkItemsController(IMediator mediator, IControllerReponseHelper<WorkItemsController> controllerReponseHelper)
        {
            _mediator = mediator;
            _controllerReponseHelper = controllerReponseHelper;
        }

        [HttpGet("instance/{id}")]
        public async Task<IActionResult> GetWorkItemByIntanceId(Guid id)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(new GetWorkItemsQuery(id)),
                nameof(GetWorkItemByIntanceId),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkItems(Guid id)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(new GetWorkItemByIdQuery(id)),
                nameof(GetWorkItems),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpPost("filtered")]
        public async Task<IActionResult> GetWorkItemsByFilter(GetWorkItemsByFilterQuery query)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(query),
                nameof(GetWorkItemsByFilter),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }
    }
}
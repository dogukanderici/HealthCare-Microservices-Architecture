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
            GetWorkItemsByFilterQuery filter = GetWorkItemsByFilterQuery.Filter(
                    query.InstanceId,
                    query.AssignedUserId,
                    query.Status,
                    query.CreatedAt,
                    query.CreatedBy
                    );

            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(filter),
                nameof(GetWorkItemsByFilter),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }
    }
}
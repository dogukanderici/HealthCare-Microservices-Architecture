using Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
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
    public class WorkItemsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<WorkItemsController> _logger;

        public WorkItemsController(IMediator mediator, ILogger<WorkItemsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("instance/{id}")]
        public async Task<IActionResult> GetWorkItemByIntanceId(Guid id)
        {
            try
            {
                List<GetWorkItemsQueryResult> result = await _mediator.Send(new GetWorkItemsQuery(id));

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(WorkItemsController),
                    nameof(GetWorkItems),
                    LogConstants.SuccessMessage.CallingSuccess);

                return Ok(GenericAPIResponse<List<GetWorkItemsQueryResult>>.SuccessAPIResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(WorkItemsController),
                    nameof(GetWorkItems),
                    ex);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkItems(Guid id)
        {
            try
            {
                GetWorkItemByIdQueryResult result = await _mediator.Send(new GetWorkItemByIdQuery(id));

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(WorkItemsController),
                    nameof(GetWorkItems),
                    LogConstants.SuccessMessage.CallingSuccess);

                return Ok(GenericAPIResponse<GetWorkItemByIdQueryResult>.SuccessAPIResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(WorkItemsController),
                    nameof(GetWorkItems),
                    ex);

                return BadRequest(GenericAPIResponse<bool>.ErrorAPIResponse());
            }
        }
    }
}
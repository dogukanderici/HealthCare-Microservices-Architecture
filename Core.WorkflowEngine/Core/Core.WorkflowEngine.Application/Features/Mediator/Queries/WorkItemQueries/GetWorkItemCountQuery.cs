using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries
{
    public class GetWorkItemCountQuery : IRequest<InternalHandlerResponse<int>>
    {
    }
}

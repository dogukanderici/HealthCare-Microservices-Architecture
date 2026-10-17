using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.InstanceQueries
{
    public class GetInstanceCountQuery : IRequest<InternalHandlerResponse<int>>
    {
    }
}

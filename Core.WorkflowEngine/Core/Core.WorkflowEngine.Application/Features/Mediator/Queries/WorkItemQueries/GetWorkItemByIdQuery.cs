using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries
{
    public class GetWorkItemByIdQuery : IRequest<InternalHandlerResponse<GetWorkItemByIdQueryResult>>, ICacheableQuery
    {
        public Guid WorkItemId { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey([
            typeof(GetWorkItemsQuery).Name,
            (WorkItemId.ToString())
            ]);

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);

        public GetWorkItemByIdQuery(Guid workItemId)
        {
            WorkItemId = workItemId;
        }
    }
}
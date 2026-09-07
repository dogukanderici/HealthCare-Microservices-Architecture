using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InboxResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.InboxQueries
{
    public class GetInboxByUserIdQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetInboxByUserIdQueryResult>>>, ICacheableQuery
    {
        public Guid AssignedUserId { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey(
            [
                typeof(GetInboxByUserIdQuery).Name,
                AssignedUserId.ToString()
            ]
        );

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);


        public GetInboxByUserIdQuery(Guid assignedUserId)
        {
            AssignedUserId = assignedUserId;
        }
    }
}
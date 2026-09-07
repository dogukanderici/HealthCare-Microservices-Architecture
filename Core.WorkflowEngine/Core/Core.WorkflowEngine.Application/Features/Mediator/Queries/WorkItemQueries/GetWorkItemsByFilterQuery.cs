using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;
using System.Text.Json.Serialization;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries
{
    public class GetWorkItemsByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetWorkItemsByFilterQueryResult>>>, ICacheableQuery
    {
        public Guid? InstanceId { get; set; }
        public Guid? AssignedUserId { get; set; }
        public int? Status { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey([
            typeof(GetWorkItemsQuery).Name,
            (InstanceId.ToString()),
            (AssignedUserId.ToString()),
            (CreatedAt.ToString()),
            (CreatedBy.ToString())
            ]);

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);

        [JsonConstructor]
        private GetWorkItemsByFilterQuery()
        {

        }

        public static GetWorkItemsByFilterQuery Filter(
            Guid? instanceId, Guid? assignedUserId, int? status, DateTimeOffset? createdAt, Guid? createdBy) =>
            new GetWorkItemsByFilterQuery()
            {
                InstanceId = instanceId,
                AssignedUserId = assignedUserId,
                Status = status,
                CreatedAt = createdAt,
                CreatedBy = createdBy
            };
    }
}
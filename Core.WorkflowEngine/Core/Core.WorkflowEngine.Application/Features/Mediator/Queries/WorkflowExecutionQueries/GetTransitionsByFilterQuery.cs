using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkflowExecutionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;
using System.Text.Json.Serialization;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkflowExecutionQueries
{
    public class GetTransitionsByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetTransitionsByFilterQueryResult>>>, ICacheableQuery
    {
        public Guid ProcessTaskId { get; set; }
        public Guid ActionId { get; set; }
        public bool IsActive { get; set; }
        public Guid VersionId { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey([
            typeof(GetTransitionsByFilterQuery).Name,
            (ProcessTaskId.ToString()),
            (ActionId.ToString()),
            (VersionId.ToString()),
            ]);

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);

        [JsonConstructor]
        private GetTransitionsByFilterQuery()
        {

        }

        public GetTransitionsByFilterQuery Filter(Guid processTaskId, Guid actionId, bool isActive, Guid VersionId) =>
            new GetTransitionsByFilterQuery
            {
                ProcessTaskId = processTaskId,
                ActionId = actionId,
                IsActive = isActive,
                VersionId = VersionId
            };
    }
}
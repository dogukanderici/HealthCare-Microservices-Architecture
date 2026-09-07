using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;
using System.Text.Json.Serialization;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskTransitionQueries
{
    public class GetProcessTaskTransitionsByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetProcessTaskTransitionsByFilterQueryResult>>>, ICacheableQuery
    {
        public Guid? ProcessTaskId { get; set; }
        public Guid? ActionId { get; set; }
        public bool? IsActive { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey(
            [
                typeof(GetProcessTaskTransitionsByFilterQuery).Name,
                (ProcessTaskId.ToString()),
                (ActionId.ToString())
            ]
        );

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);

        [JsonConstructor]
        private GetProcessTaskTransitionsByFilterQuery()
        {

        }

        public static GetProcessTaskTransitionsByFilterQuery Filter(Guid? processTaskId, Guid? actionId, bool? isActive) =>
            new GetProcessTaskTransitionsByFilterQuery
            {
                ProcessTaskId = processTaskId,
                ActionId = actionId,
                IsActive = isActive
            };
    }
}
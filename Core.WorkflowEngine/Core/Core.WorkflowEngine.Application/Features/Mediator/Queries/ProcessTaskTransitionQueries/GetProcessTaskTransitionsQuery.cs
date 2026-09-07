using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskTransitionQueries
{
    public class GetProcessTaskTransitionsQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetProcessTaskTransitionsQueryResult>>>, ICacheableQuery
    {
        public Guid ProcessTaskId { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey(
            [
                typeof(GetProcessTaskTransitionsQuery).Name,
                (ProcessTaskId.ToString())
            ]
        );

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);

        public GetProcessTaskTransitionsQuery(Guid processTaskId)
        {
            ProcessTaskId = processTaskId;
        }
    }
}
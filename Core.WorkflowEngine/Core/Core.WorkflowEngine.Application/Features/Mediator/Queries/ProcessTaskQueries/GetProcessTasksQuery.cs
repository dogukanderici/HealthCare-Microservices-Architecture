using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskQueries
{
    public class GetProcessTasksQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetProcessTasksQueryResult>>>, ICacheableQuery
    {
        public Guid ProcessId { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey(
            [
                typeof(GetProcessTasksQuery).Name,
                (ProcessId.ToString())
            ]
        );

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);

        public GetProcessTasksQuery(Guid processId)
        {
            ProcessId = processId;
        }
    }
}

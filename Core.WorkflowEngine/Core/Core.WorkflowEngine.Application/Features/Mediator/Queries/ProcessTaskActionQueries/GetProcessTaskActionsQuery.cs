using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskActionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskActionQueries
{
    public class GetProcessTaskActionsQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetProcessTaskActionsQueryResult>>>, ICacheableQuery
    {
        public string CacheKey => CacheKeyGenerator.GenerateCacheKey(typeof(GetProcessTaskActionsQuery).Name);

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);
    }
}
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.InstanceQueries
{
    public class GetInstancesQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetInstancesQueryResult>>>, ICacheableQuery
    {
        public string CacheKey => CacheKeyGenerator.GenerateCacheKey(nameof(GetInstancesQuery));

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);
    }
}

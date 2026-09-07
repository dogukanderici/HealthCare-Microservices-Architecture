using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries
{
    public class GetProcessDefinitionsQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsQueryResult>>>, ICacheableQuery
    {
        public string CacheKey => CacheKeyGenerator.GenerateCacheKey(typeof(GetProcessDefinitionsQuery).Name);

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);
    }
}

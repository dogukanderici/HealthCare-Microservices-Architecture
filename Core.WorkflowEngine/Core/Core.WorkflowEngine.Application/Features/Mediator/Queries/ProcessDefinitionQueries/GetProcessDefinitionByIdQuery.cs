using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries
{
    public class GetProcessDefinitionByIdQuery : IRequest<InternalHandlerResponse<GetProcessDefinitionByIdQueryResult>>, ICacheableQuery
    {
        public Guid Id { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey(
            [
                typeof(GetProcessDefinitionByIdQuery).Name,
                (Id.ToString())
            ]
        );

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);

        public object CacheKeyGeneratorx { get; private set; }

        public GetProcessDefinitionByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

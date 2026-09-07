using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.InstanceQueries
{
    public class GetInstanceByIdQuery : IRequest<InternalHandlerResponse<GetInstanceByIdQueryResult>>, ICacheableQuery
    {
        public Guid Id { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey(
            [
                typeof(GetInstanceByIdQuery).Name,
                Id.ToString()
            ]
        );

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);


        public GetInstanceByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

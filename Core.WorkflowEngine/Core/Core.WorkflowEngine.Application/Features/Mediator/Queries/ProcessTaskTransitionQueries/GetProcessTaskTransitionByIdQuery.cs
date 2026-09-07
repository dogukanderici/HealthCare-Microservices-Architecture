using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskTransitionQueries
{
    public class GetProcessTaskTransitionByIdQuery : IRequest<InternalHandlerResponse<GetProcessTaskTransitionByIdQueryResult>>, ICacheableQuery
    {
        public Guid Id { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey(
            [
                typeof(GetProcessTaskTransitionByIdQuery).Name,
                (Id.ToString())
            ]
        );

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);

        public GetProcessTaskTransitionByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskActionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskActionQueries
{
    public class GetProcessTaskActionByIdQuery : IRequest<InternalHandlerResponse<GetProcessTaskActionByIdQueryResult>>, ICacheableQuery
    {
        public Guid Id { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey(
            [
                typeof(GetProcessTaskActionByIdQuery).Name,
                (Id.ToString())
            ]
        );

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);

        public GetProcessTaskActionByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskQueries
{
    public class GetProcessTaskByIdQuery : IRequest<InternalHandlerResponse<GetProcessTaskByIdQueryResult>>, ICacheableQuery
    {
        public Guid Id { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey(
            [
                typeof(GetProcessTaskByIdQuery).Name,
                (Id.ToString())
            ]
        );

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);

        public GetProcessTaskByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
using Core.WorkflowEngine.Application.Features.Mediator.Results.InboxResults;
using Core.WorkflowEngine.Application.Features.Wrappers;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.InboxQueries
{
    public class GetInboxByUserIdQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetInboxByUserIdQueryResult>>>, ICacheableQuery
    {
        public Guid AssignedUserId { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey(
            [
                typeof(GetInboxByUserIdQuery).Name,
                AssignedUserId.ToString()
            ]
        );

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);


        public GetInboxByUserIdQuery(Guid assignedUserId)
        {
            AssignedUserId = assignedUserId;
        }
    }
}
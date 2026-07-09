using Core.WorkflowEngine.Application.Features.Mediator.Results.InboxResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.InboxQueries
{
    public class GetInboxByUserIdQuery : IRequest<InternalHandlerResponse<List<GetInboxByUserIdQueryResult>>>
    {
        public Guid AssignedUserId { get; set; }

        public GetInboxByUserIdQuery(Guid assignedUserId)
        {
            AssignedUserId = assignedUserId;
        }
    }
}
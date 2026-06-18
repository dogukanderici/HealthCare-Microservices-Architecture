using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries
{
    public class GetWorkItemsByFilterQuery : IRequest<List<GetWorkItemsByFilterQueryResult>>
    {
        public Guid? InstanceId { get; set; }
        public Guid? AssignedUserId { get; set; }
        public int? Status { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }

        private GetWorkItemsByFilterQuery()
        {

        }

        public static GetWorkItemsByFilterQuery Filter(
            Guid? instanceId, Guid? assignedUserId, int? status, DateTimeOffset? createdAt, Guid? createdBy) =>
            new GetWorkItemsByFilterQuery()
            {
                InstanceId = instanceId,
                AssignedUserId = assignedUserId,
                Status = status,
                CreatedAt = createdAt,
                CreatedBy = createdBy
            };
    }
}
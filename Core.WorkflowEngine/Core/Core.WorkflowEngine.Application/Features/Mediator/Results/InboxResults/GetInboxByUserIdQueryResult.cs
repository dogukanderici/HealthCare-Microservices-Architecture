using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Results.InboxResults
{
    public class GetInboxByUserIdQueryResult
    {
        public Guid Id { get; set; }
        public Guid InstanceId { get; set; }
        public Guid StepId { get; set; }
        public Guid AssignedUserId { get; set; }
        public Guid AssignedRoleId { get; set; }

        public GetInboxWithInstanceResult Instance { get; set; }
        public GetWorkItemWithProcessTaskResult ProcessTask { get; set; }

    }
}

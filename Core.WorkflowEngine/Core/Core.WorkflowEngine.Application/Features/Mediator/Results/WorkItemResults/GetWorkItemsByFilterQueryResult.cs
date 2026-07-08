using Core.WorkflowEngine.Application.Features.Mediator.Results.GenericResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults
{
    public class GetWorkItemsByFilterQueryResult : GenericAuditResults
    {
        public Guid Id { get; set; }
        public Guid InstanceId { get; set; }
        public Guid StepId { get; set; }
        public Guid TaskId { get; set; }
        public Guid AssignedUserId { get; set; }
        public Guid AssignedRoleId { get; set; }
        public Guid SelectedAction { get; set; }
        public DateTimeOffset CompletedAt { get; set; }
        public Guid CompletedBy { get; set; }
        public int Status { get; set; }
    }
}

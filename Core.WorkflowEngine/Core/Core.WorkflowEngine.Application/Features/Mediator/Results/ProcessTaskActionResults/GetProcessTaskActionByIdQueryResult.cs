using Core.WorkflowEngine.Application.Features.Mediator.Results.GenericResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskActionResults
{
    public class GetProcessTaskActionByIdQueryResult : GenericAuditResults
    {
        public Guid Id { get; set; }
        public Guid ProcessTaskId { get; set; }
        public Guid AssignedUser { get; set; }
        public Guid ActionId { get; set; }
        public string ActionName { get; set; }
        public int ActionType { get; set; }
        public int ExecutionOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
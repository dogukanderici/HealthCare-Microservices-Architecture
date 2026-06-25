using Core.WorkflowEngine.Application.Features.Mediator.Results.GenericResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults
{
    public class GetProcessTaskTransitionByIdQueryResult : GenericAuditResults
    {
        public Guid Id { get; set; }
        public Guid ProcessTaskId { get; set; }
        public Guid NextTaskId { get; set; }
        public Guid ActionId { get; set; }
        public string ConditionExpression { get; set; }
        public bool IsActive { get; set; }
    }
}

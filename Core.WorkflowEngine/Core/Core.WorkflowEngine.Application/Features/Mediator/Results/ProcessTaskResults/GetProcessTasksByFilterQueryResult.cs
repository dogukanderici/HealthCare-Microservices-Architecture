using Core.WorkflowEngine.Application.Features.Mediator.Results.GenericResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskResults
{
    public class GetProcessTasksByFilterQueryResult : GenericAuditResults
    {
        public Guid Id { get; set; }
        public Guid ProcessId { get; set; }
        public string StepName { get; set; }
        public bool IsStartStep { get; set; }
        public bool IsActive { get; set; }
    }
}
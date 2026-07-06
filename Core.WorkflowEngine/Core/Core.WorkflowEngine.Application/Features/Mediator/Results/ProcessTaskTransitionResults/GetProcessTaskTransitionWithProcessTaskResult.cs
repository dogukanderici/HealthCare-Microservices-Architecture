using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults
{
    public class GetProcessTaskTransitionWithProcessTaskResult
    {
        public Guid ProcessId { get; set; }
        public Guid AssignedUser { get; set; }
        public string StepName { get; set; }
    }
}
using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Results.InboxResults
{
    public class GetInboxWithInstanceResult
    {
        public Guid ProcessId { get; set; }
        public int Number { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }

        public GetInstanceWithProcessResult ProcessDefinition{ get; set; }
    }
}
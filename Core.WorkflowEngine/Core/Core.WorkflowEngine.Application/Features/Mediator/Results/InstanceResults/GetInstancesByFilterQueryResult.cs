using Core.WorkflowEngine.Application.Features.Mediator.Results.GenericResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults
{
    public class GetInstancesByFilterQueryResult : GenericAuditResults
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public int Number { get; set; }
        public Guid InitiatorWorkItemId { get; set; }
        public int Status { get; set; }
    }
}

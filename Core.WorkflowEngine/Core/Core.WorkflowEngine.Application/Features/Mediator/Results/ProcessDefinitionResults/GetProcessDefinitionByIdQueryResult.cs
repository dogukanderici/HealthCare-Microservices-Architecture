using Core.WorkflowEngine.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults
{
    public class GetProcessDefinitionByIdQueryResult : GenericAuditProperty
    {
        public Guid Id { get; set; }
        public string ProcessName { get; set; }
        public bool IsActive { get; set; }
    }
}

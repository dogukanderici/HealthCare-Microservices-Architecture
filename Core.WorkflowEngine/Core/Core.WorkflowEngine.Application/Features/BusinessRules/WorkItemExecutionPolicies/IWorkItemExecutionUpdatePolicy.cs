using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkflowExecutionCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.WorkItemExecutionPolicies
{
    public interface IWorkItemExecutionUpdatePolicy : IPolicyRule<CommitWorkItemExecutionCommand>
    {
    }
}

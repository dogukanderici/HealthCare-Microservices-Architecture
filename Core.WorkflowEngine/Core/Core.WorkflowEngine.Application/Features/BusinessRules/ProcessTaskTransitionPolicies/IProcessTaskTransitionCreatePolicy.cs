using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskTransitionPolicies
{
    public interface IProcessTaskTransitionCreatePolicy : IPolicyRule<ProcessTaskTransition>
    {
    }
}
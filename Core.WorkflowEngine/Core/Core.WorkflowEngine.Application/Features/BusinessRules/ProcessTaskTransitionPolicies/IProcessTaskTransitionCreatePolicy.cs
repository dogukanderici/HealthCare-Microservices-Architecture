using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskTransitionPolicies
{
    public interface IProcessTaskTransitionCreatePolicy
    {
        Task<InternalBusinessRuleResponse<bool>> ExistingTaskTransitionControlAsync(DBQueryOptions<ProcessTaskTransition> queryData);

        Task<InternalBusinessRuleResponse<bool>> CheckAllRulesForUpdateAsync(ProcessTaskTransition entity);
        Task<InternalBusinessRuleResponse<ProcessTaskTransition>> CheckAllRulesForDeleteAsync(Guid id);
    }
}
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskTransitionRules
{
    public interface ITaskTransitionBusinessRule
    {
        Task<InternalBusinessRuleResponse<bool>> ExistingTaskTransitionControlAsync(DBQueryOptions<ProcessTaskTransition> queryData);

        Task<InternalBusinessRuleResponse<bool>> CheckAllRulesForUpdateAsync(ProcessTaskTransition entity);
        Task<InternalBusinessRuleResponse<ProcessTaskTransition>> CheckAllRulesForDeleteAsync(Guid id);
    }
}
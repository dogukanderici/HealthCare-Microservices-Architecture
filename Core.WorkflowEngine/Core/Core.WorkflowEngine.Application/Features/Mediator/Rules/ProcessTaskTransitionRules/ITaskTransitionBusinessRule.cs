using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskTransitionRules
{
    public interface ITaskTransitionBusinessRule
    {
        Task<InternalBusinessRuleResponse<bool>> ExistingTaskTransitionControlAsync(DBQueryOptions<ProcessTaskTransition> queryData);

        Task<InternalBusinessRuleResponse<bool>> CheckAllRulesForUpdateAsync(ProcessTaskTransition entity);
        Task<InternalBusinessRuleResponse<ProcessTaskTransition>> CheckAllRulesForDeleteAsync(Guid id);
    }
}
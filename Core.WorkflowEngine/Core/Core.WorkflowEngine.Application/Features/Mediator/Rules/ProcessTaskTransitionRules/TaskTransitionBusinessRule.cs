using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskTransitionRules
{
    public class TaskTransitionBusinessRule : ITaskTransitionBusinessRule
    {
        private readonly IBaseBusinessRule<ProcessTaskTransition, DBQueryOptions<ProcessTaskTransition>> _baseBusinessRule;

        public TaskTransitionBusinessRule(IBaseBusinessRule<ProcessTaskTransition, DBQueryOptions<ProcessTaskTransition>> baseBusinessRule)
        {
            _baseBusinessRule = baseBusinessRule;
        }

        public async Task<InternalBusinessRuleResponse<bool>> ExistingTaskTransitionControlAsync(DBQueryOptions<ProcessTaskTransition> queryData)
        {
            int data = await _baseBusinessRule.ExistingDataControlAsync(queryData);

            return InternalBusinessRuleResponse<bool>.Success((data == 0));
        }

        public async Task<InternalBusinessRuleResponse<bool>> CheckAllRulesForUpdateAsync(ProcessTaskTransition entity)
        {
            DBQueryOptions<ProcessTaskTransition> dBQueryOptions = new DBQueryOptions<ProcessTaskTransition>();

            Expression<Func<ProcessTaskTransition, bool>> filter = x => x.Id == entity.Id;
            dBQueryOptions.filter = filter;

            InternalBusinessRuleResponse<bool> checkExist = await ExistingTaskTransitionControlAsync(dBQueryOptions);

            return InternalBusinessRuleResponse<bool>.Success((checkExist.Data));
        }

        public async Task<InternalBusinessRuleResponse<ProcessTaskTransition>> CheckAllRulesForDeleteAsync(Guid id)
        {
            DBQueryOptions<ProcessTaskTransition> dBQueryOptions = new DBQueryOptions<ProcessTaskTransition>();

            Expression<Func<ProcessTaskTransition, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            ProcessTaskTransition existData = await _baseBusinessRule.ExistingDataAsync(dBQueryOptions);

            return InternalBusinessRuleResponse<ProcessTaskTransition>.Success(existData);
        }
    }
}
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessDefinitionBusinessRules
{
    public class ProcessDefinitionBusinessRule : IProcessDefinitionBusinessRule
    {
        private readonly IRepository<ProcessDefinition> _repository;
        private readonly IBaseBusinessRule<ProcessDefinition, DBQueryOptions<ProcessDefinition>> _businessRule;

        public ProcessDefinitionBusinessRule(IRepository<ProcessDefinition> repository, IBaseBusinessRule<ProcessDefinition, DBQueryOptions<ProcessDefinition>> businessRule)
        {
            _repository = repository;
            _businessRule = businessRule;
        }

        public async Task<bool> ExistingProcessDefinitionDataAsync(Guid id)
        {
            DBQueryOptions<ProcessDefinition> dbQueryOptions = new DBQueryOptions<ProcessDefinition>();

            Expression<Func<ProcessDefinition, bool>> filter = x => x.Id == id;
            dbQueryOptions.filter = filter;

            int data = await _businessRule.ExistingDataControlAsync(dbQueryOptions);

            return data == 0;
        }
    }
}
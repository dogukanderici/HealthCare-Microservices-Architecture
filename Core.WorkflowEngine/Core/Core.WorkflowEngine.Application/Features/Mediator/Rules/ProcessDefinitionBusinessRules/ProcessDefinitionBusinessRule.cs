using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
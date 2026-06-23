using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> ExistingProcessDefinitionDataAsync(DBQueryOptions<ProcessDefinition> dbQueryOptions)
        {
            int data = await _businessRule.ExistingDataControlAsync(dbQueryOptions);

            return data == 0;
        }
    }
}
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskActionBusinessRules
{
    public class ProcessTaskActionBusinessRule : IProcessTaskActionBusinessRule
    {
        private readonly IRepository<ProcessTaskAction> _repository;
        private readonly IBaseBusinessRule<ProcessTaskAction, DBQueryOptions<ProcessTaskAction>> _businessRule;
        private readonly ILogger<ProcessTaskActionBusinessRule> _logger;

        public ProcessTaskActionBusinessRule(IRepository<ProcessTaskAction> repository, IBaseBusinessRule<ProcessTaskAction, DBQueryOptions<ProcessTaskAction>> businessRule, ILogger<ProcessTaskActionBusinessRule> logger)
        {
            _repository = repository;
            _businessRule = businessRule;
            _logger = logger;
        }

        public async Task<bool> CheckExistingDataAsync(DBQueryOptions<ProcessTaskAction> dBQueryOptions)
        {
            int data = await _businessRule.ExistingDataControlAsync(dBQueryOptions);
            return data == 0;
        }

        public async Task<bool> CheckAllRulesAsync(DBQueryOptions<ProcessTaskAction> dBQueryOptions)
        {
            bool existRule = await CheckExistingDataAsync(dBQueryOptions);
            return existRule;
        }
    }
}
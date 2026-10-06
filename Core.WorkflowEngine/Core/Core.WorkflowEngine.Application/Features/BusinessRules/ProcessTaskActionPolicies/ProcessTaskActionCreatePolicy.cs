using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskActionPolicies
{
    public class ProcessTaskActionCreatePolicy : IProcessTaskActionCreatePolicy
    {
        private readonly IRepository<ProcessTaskAction> _repository;
        private readonly IBaseBusinessRule<ProcessTaskAction, DBQueryOptions<ProcessTaskAction>> _businessRule;
        private readonly ILogger<ProcessTaskActionCreatePolicy> _logger;

        public ProcessTaskActionCreatePolicy(IRepository<ProcessTaskAction> repository, IBaseBusinessRule<ProcessTaskAction, DBQueryOptions<ProcessTaskAction>> businessRule, ILogger<ProcessTaskActionCreatePolicy> logger)
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
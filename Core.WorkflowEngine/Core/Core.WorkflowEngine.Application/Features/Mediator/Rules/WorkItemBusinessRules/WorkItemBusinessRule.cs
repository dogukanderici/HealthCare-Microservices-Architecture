using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.WorkItemBusinessRules
{
    public class WorkItemBusinessRule : IWorkItemBusinessRule
    {
        private readonly IBaseBusinessRule<WorkItem, DBQueryOptions<WorkItem>> _businessRule;

        public WorkItemBusinessRule(IBaseBusinessRule<WorkItem, DBQueryOptions<WorkItem>> businessRule)
        {
            _businessRule = businessRule;
        }

        public async Task<bool> CheckExistingDataAsync(DBQueryOptions<WorkItem> dBQueryOptions)
        {
            int data = await _businessRule.ExistingDataControlAsync(dBQueryOptions);

            return data == 0;
        }

        public async Task<bool> CheckAllRulesAsync(DBQueryOptions<WorkItem> dBQueryOptions)
        {
            bool checkExist = await CheckExistingDataAsync(dBQueryOptions);

            return checkExist;
        }
    }
}
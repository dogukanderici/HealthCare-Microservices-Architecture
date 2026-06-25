using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.WorkItemBusinessRules
{
    public interface IWorkItemBusinessRule
    {
        Task<bool> CheckExistingDataAsync(DBQueryOptions<WorkItem> dBQueryOptions);
        Task<bool> CheckAllRulesAsync(DBQueryOptions<WorkItem> dBQueryOptions);
    }
}

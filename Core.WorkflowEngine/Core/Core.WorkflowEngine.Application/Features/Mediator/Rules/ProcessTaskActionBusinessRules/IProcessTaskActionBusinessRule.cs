using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskActionBusinessRules
{
    public interface IProcessTaskActionBusinessRule
    {
        Task<bool> CheckExistingDataAsync(DBQueryOptions<ProcessTaskAction> dBQueryOptions);
        Task<bool> CheckAllRulesAsync(DBQueryOptions<ProcessTaskAction> dBQueryOptions);
    }
}

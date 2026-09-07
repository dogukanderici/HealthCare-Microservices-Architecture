using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskActionBusinessRules
{
    public interface IProcessTaskActionBusinessRule
    {
        Task<bool> CheckExistingDataAsync(DBQueryOptions<ProcessTaskAction> dBQueryOptions);
        Task<bool> CheckAllRulesAsync(DBQueryOptions<ProcessTaskAction> dBQueryOptions);
    }
}

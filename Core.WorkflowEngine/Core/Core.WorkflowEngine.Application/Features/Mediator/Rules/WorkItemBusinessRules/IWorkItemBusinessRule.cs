using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.WorkItemBusinessRules
{
    public interface IWorkItemBusinessRule
    {
        Task<bool> CheckExistingDataAsync(DBQueryOptions<WorkItem> dBQueryOptions);
        Task<bool> CheckAllRulesAsync(DBQueryOptions<WorkItem> dBQueryOptions);
    }
}

using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.WorkItemPolicies
{
    public interface IWorkItemCreatePolicy
    {
        Task<bool> CheckExistingDataAsync(DBQueryOptions<WorkItem> dBQueryOptions);
        Task<bool> CheckAllRulesAsync(DBQueryOptions<WorkItem> dBQueryOptions);
    }
}

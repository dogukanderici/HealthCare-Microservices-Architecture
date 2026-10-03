using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.WorkItemBusinessRules
{
    public interface IWorkItemBusinessRule
    {
        Task<bool> CheckExistingDataAsync(DBQueryOptions<WorkItem> dBQueryOptions);
        Task<bool> CheckAllRulesAsync(DBQueryOptions<WorkItem> dBQueryOptions);
    }
}

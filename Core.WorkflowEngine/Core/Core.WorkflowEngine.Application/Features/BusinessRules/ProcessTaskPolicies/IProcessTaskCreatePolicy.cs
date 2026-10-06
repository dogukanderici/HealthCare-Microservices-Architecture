using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskPolicies
{
    public interface IProcessTaskCreatePolicy
    {
        Task<bool> CheckExistingDataAsync(DBQueryOptions<ProcessTask> dBQueryOptions);
    }
}

using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskBusinessRules
{
    public interface IProcessTaskBusinessRule
    {
        Task<bool> CheckExistingDataAsync(DBQueryOptions<ProcessTask> dBQueryOptions);
    }
}

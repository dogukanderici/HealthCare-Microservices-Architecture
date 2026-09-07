using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskBusinessRules
{
    public interface IProcessTaskBusinessRule
    {
        Task<bool> CheckExistingDataAsync(DBQueryOptions<ProcessTask> dBQueryOptions);
    }
}

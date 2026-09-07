using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.InstanceBusinessRules
{
    public interface IInstanceBusinessRule
    {
        Task<bool> ExistingInstanceControlAsync(DBQueryOptions<Instance> queryData);


        Task<bool> CheckAllRulesAsync(DBQueryOptions<Instance> queryData);
    }
}

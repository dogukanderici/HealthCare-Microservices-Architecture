using Core.WorkflowEngine.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.InstanceBusinessRules
{
    public interface IInstanceBusinessRule<TEntity, TQueryData>
    {
        Task<bool> ExistingInstanceControlAsync(TQueryData queryData);
        

        Task<bool> CheckAllRulesAsync(TQueryData queryData);
    }
}

using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.InstanceBusinessRules
{
    public interface IInstanceBusinessRule
    {
        Task<bool> ExistingInstanceControlAsync(DBQueryOptions<Instance> queryData);


        Task<bool> CheckAllRulesAsync(DBQueryOptions<Instance> queryData);
    }
}

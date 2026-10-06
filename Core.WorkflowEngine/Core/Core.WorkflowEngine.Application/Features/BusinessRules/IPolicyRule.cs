using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.BusinessRules
{
    public interface IPolicyRule<T>
    {
        Task<InternalPolicyResponse> ExecuteAllRuleAsync(T entity);
    }
}

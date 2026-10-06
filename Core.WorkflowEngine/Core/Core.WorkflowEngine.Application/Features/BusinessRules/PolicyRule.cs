using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.BusinessRules
{
    public abstract class PolicyRule<TEntity> : IPolicyRule<TEntity>
    {

        // PolicyRule miras alan her class abstract metotlarını kendine göre yazmak zorundadır (override etmek zorunda.)
        // Interface üzerinden ExecuteAllRuleAsync metotuna erişilebilir sadece.
        protected abstract Task<InternalPolicyResponse> CountExistingDataAsync(TEntity entity);

        public abstract Task<InternalPolicyResponse> ExecuteAllRuleAsync(TEntity entity);
    }
}
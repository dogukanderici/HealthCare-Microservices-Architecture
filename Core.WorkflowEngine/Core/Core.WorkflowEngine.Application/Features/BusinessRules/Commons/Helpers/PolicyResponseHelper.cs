using Core.WorkflowEngine.Application.Commons.CustomExceptions;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Helpers
{
    public class PolicyResponseHelper : List<Func<Task<InternalPolicyResponse>>>
    {
        public async Task<InternalPolicyResponse> ExecutePolicyRulesAync()
        {
            foreach (var rule in this)
            {
                InternalPolicyResponse result = await rule();

                if (!result.IsSuccess)
                    throw new BusinessRuleException($"INVALID BUSINESS RULE. MESSAGE: {result.PolicyMessage}", result.PolicyMessage);
            }

            return InternalPolicyResponse.Success();
        }
    }
}
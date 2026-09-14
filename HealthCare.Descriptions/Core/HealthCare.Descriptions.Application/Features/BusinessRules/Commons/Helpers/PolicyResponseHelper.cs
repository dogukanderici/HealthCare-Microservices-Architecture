using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Helpers
{
    public class PolicyResponseHelper : List<Func<Task<InternalPolicyResponse>>>
    {
        public async Task<InternalPolicyResponse> ToPolicyResponseAsync()
        {
            foreach (var rule in this)
            {
                InternalPolicyResponse policyResult = await rule();

                if (!policyResult.IsSuccess)
                {
                    return InternalPolicyResponse.Failure(policyResult.BusinessRuleError);
                }
            }

            return InternalPolicyResponse.Success();
        }
    }
}
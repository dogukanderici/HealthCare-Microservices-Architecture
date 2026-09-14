using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Extensions
{
    public static class InternalServiceResponseExtension
    {

        public static InternalPolicyResponse ToPolicyResponse(this InternalServiceResponse<int> service, Func<int, bool> checkingCondition, string message)
        {
            if (service.IsSuccess)
            {
                if (checkingCondition(service.Data))
                {
                    return InternalPolicyResponse.Failure(message);
                }

                return InternalPolicyResponse.Success();
            }

            return InternalPolicyResponse.Failure(service.ServiceMessage);
        }
    }
}
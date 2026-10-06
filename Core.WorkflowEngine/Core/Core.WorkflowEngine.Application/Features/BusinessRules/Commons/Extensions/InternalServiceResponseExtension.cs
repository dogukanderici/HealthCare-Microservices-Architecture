using Core.WorkflowEngine.Application.Commons.CustomExceptions;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Extensions
{
    public static class InternalServiceResponseExtension
    {
        public static InternalPolicyResponse ExecuteServiceWithPolicy(this InternalServiceResponse<int> service, Func<int, bool> checkingCondition, string message)
        {
            if (service.IsSuccess)
            {
                if (checkingCondition(service.Data))
                {
                    return InternalPolicyResponse.Failure(message);
                }

                return InternalPolicyResponse.Success();
            }

            throw new InternalServiceException("An error occured while executing policy rule", service.ServiceMessage);
        }
    }
}
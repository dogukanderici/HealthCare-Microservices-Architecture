using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper
{
    public class InternalPolicyResponse
    {
        public bool IsSuccess { get; set; }
        public string PolicyMessage { get; set; }


        [JsonConstructor]
        private InternalPolicyResponse()
        {

        }

        public static InternalPolicyResponse Success(string message = "Policy rules is valid!") =>
            new InternalPolicyResponse
            {
                IsSuccess = true,
                PolicyMessage = message
            };

        public static InternalPolicyResponse Failure(string message) =>
            new InternalPolicyResponse
            {
                IsSuccess = false,
                PolicyMessage = message
            };
    }
}
using HealthCare.Descriptions.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses
{
    public class InternalPolicyResponse : IInternalPolicyResponse
    {
        public bool IsSuccess { get; set; }
        public string? BusinessRuleError { get; set; }
        public List<string>? BusinessRuleErrors { get; set; }

        [JsonConstructor]
        private InternalPolicyResponse()
        {

        }

        public static InternalPolicyResponse Response(bool isSucess, string? businessRuleError) =>
            new InternalPolicyResponse
            {
                IsSuccess = isSucess,
                BusinessRuleError = businessRuleError,
                BusinessRuleErrors = default
            };

        public static InternalPolicyResponse Success() =>
            new InternalPolicyResponse
            {
                IsSuccess = true,
                BusinessRuleError = default,
                BusinessRuleErrors = default
            };

        public static InternalPolicyResponse Failure(List<string>? businessRuleErrors) =>
            new InternalPolicyResponse
            {
                IsSuccess = false,
                BusinessRuleError = default,
                BusinessRuleErrors = businessRuleErrors
            };

    }
}
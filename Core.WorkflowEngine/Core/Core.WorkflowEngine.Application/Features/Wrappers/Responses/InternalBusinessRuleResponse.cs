using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Wrappers.Responses
{
    public class InternalBusinessRuleResponse<T>
    {
        public T Data { get; private set; }
        public string RuleMessage { get; private set; }

        public static InternalBusinessRuleResponse<T> Success(T data, string ruleMessage = "Success")
        {
            return new InternalBusinessRuleResponse<T>
            {
                Data = data,
                RuleMessage = ruleMessage
            };
        }
    }
}
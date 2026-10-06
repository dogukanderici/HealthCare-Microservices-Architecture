using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Commons.CustomExceptions
{
    public class BusinessRuleException : Exception
    {
        public string Error { get; set; }

        public BusinessRuleException(string message, string error) : base(error)
        {
            Error = error;
        }
    }
}
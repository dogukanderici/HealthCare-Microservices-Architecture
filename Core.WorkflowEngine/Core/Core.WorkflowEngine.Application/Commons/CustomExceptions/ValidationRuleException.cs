using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Commons.CustomExceptions
{
    // TODO - true - false dönen bir yapıdan business rule gibi throw atan bir yapıya güncellenecek.
    public class ValidationRuleException : Exception
    {
        public string Error { get; set; }

        public ValidationRuleException(string message, string error) : base(message)
        {
            Error = error;
        }
    }
}
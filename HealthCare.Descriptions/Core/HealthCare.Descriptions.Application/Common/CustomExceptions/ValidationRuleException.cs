using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Common.CustomExceptions
{
    public class ValidationRuleException : Exception
    {
        public IEnumerable<string> Errors;

        public ValidationRuleException(string message, IEnumerable<string> errors) : base(message)
        {
            Errors = errors;
        }
    }
}
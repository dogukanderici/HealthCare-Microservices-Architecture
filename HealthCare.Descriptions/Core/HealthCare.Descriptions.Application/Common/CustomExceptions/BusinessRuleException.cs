using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Common.CustomExceptions
{
    public class BusinessRuleException : Exception
    {
        public string Error;

        public BusinessRuleException(string message, string error) : base(message)
        {
            Error = error;
        }
    }
}
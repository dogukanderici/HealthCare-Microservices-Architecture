using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Commons.CustomExceptions
{
    public class InternalServiceException : Exception
    {
        public string Error { get; set; }

        public InternalServiceException(string message, string error) : base(error)
        {
            Error = error;
        }
    }
}
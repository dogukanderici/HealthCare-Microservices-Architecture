using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Configuration.Wrappers
{
    public class InternalServiceResponse<T>
    {
        public bool IsSuccess { get; private set; }
        public string ServiceMessage { get; private set; }
        public T Data { get; private set; }

        public static InternalServiceResponse<T> Success(T data, string message = "Success")
        {
            return new InternalServiceResponse<T>
            {
                IsSuccess = true,
                ServiceMessage = message,
                Data = data
            };
        }

        public static InternalServiceResponse<T> Failure(string message = "Fail")
        {
            return new InternalServiceResponse<T>
            {
                IsSuccess = false,
                ServiceMessage = message,
                Data = default!
            };
        }
    }
}
using Core.WorkflowEngine.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Wrappers.Responses
{
    public class InternalCommandResponse<T> : IInternalCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string InternalMessage { get; set; }
        public T Data { get; private set; }

        public static InternalCommandResponse<T> Success(T data, string internalMessage = "Success")
        {
            return new InternalCommandResponse<T>
            {
                IsSuccess = true,
                InternalMessage = internalMessage,
                Data = data
            };
        }

        public static InternalCommandResponse<T> Failure(string internalMessage = "Fail")
        {
            return new InternalCommandResponse<T>
            {
                IsSuccess = false,
                InternalMessage = internalMessage,
                Data = default!
            };
        }
    }
}
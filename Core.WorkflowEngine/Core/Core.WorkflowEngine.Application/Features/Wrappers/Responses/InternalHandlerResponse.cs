using Core.WorkflowEngine.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Wrappers.Responses
{
    public class InternalHandlerResponse<T> : IInternalCommandResponse, IValidationResult
    {
        public bool IsSuccess { get; set; }
        public string InternalMessage { get; set; }
        public T Data { get; private set; }
        public List<string>? ValidationErrors { get; set; }

        public static InternalHandlerResponse<T> Success(T data, string internalMessage = "Success")
        {
            return new InternalHandlerResponse<T>
            {
                IsSuccess = true,
                InternalMessage = internalMessage,
                Data = data
            };
        }

        public static InternalHandlerResponse<T> Failure(string internalMessage = "Fail")
        {
            return new InternalHandlerResponse<T>
            {
                IsSuccess = false,
                InternalMessage = internalMessage,
                Data = default!
            };
        }
    }
}
using Core.WorkflowEngine.Application.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonConstructorAttribute = System.Text.Json.Serialization.JsonConstructorAttribute;

namespace Core.WorkflowEngine.Application.Features.Wrappers.Responses
{
    public class InternalHandlerResponse<T> : IInternalCommandResponse, IValidationResult
    {
        public bool IsSuccess { get; set; }
        public string InternalMessage { get; set; }

        [JsonProperty]
        public T Data { get; private set; }
        public List<string>? ValidationErrors { get; set; }

        [JsonConstructor]
        private InternalHandlerResponse()
        {
            
        }

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
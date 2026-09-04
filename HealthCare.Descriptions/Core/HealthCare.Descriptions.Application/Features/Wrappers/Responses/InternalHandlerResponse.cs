using HealthCare.Descriptions.Application.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonConstructorAttribute = System.Text.Json.Serialization.JsonConstructorAttribute;

namespace HealthCare.Descriptions.Application.Features.Wrappers.Responses
{
    public class InternalHandlerResponse<T> : IInternalHandlerResponse
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

        public static InternalHandlerResponse<T> Success(T data, string message = "Success")
        {
            return new InternalHandlerResponse<T>
            {
                IsSuccess = true,
                InternalMessage = message,
                Data = data
            };
        }

        public static InternalHandlerResponse<T> Failure(string message = "Failure")
        {
            return new InternalHandlerResponse<T>
            {
                IsSuccess = false,
                InternalMessage = message,
                Data = default
            };
        }
    }
}
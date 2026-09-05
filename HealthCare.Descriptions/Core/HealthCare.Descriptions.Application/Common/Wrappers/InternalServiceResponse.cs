using HealthCare.Descriptions.Application.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonConstructorAttribute = System.Text.Json.Serialization.JsonConstructorAttribute;

namespace HealthCare.Descriptions.Application.Common.Wrappers
{
    public class InternalServiceResponse<T> : IInternalServiceResponse
    {
        public bool IsSuccess { get; set; }
        public string ServiceMessage { get; set; }

        [JsonProperty]
        public T Data { get; private set; }

        [JsonConstructor]
        private InternalServiceResponse()
        {

        }

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
                Data = default
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonConstructorAttribute = System.Text.Json.Serialization.JsonConstructorAttribute;

namespace HealthCare.Descriptions.Application.Common.Parameters
{
    public class CryptionResponse<T>
    {
        [JsonProperty]
        public T TokenPayload { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        [JsonConstructor]
        private CryptionResponse()
        {

        }

        public static CryptionResponse<T> Success(T payload, string message = "Successful")
        {
            return new CryptionResponse<T>
            {
                TokenPayload = payload,
                Message = message,
                IsSuccess = true
            };
        }

        public static CryptionResponse<T> Fail(string message = "Failure")
        {
            return new CryptionResponse<T>
            {
                TokenPayload = default,
                Message = message,
                IsSuccess = false
            };
        }
    }
}
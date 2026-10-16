using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonConstructorAttribute = System.Text.Json.Serialization.JsonConstructorAttribute;

namespace Core.WorkflowEngine.Application.Commons.Wrappers
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

        public static CryptionResponse<T> Success(T payload, string message = "Success") =>
            new CryptionResponse<T>
            {
                TokenPayload = payload,
                IsSuccess = true,
                Message = message
            };

        public static CryptionResponse<T> Fail(string message = "Fail") =>
            new CryptionResponse<T>
            {
                TokenPayload = default,
                IsSuccess = true,
                Message = message
            };
    }
}
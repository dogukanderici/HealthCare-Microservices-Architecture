using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonConstructorAttribute = System.Text.Json.Serialization.JsonConstructorAttribute;

namespace Core.WebUI.Application.Commons.Parameters
{
    public class UIApiResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        [JsonProperty]
        public T Data { get; set; }


        [JsonConstructor]
        private UIApiResponse()
        {

        }

        public static UIApiResponse<T> Success(T data, string message = "Success")
        {
            return new UIApiResponse<T>
            {
                Status = true,
                Message = message,
                Data = data
            };
        }

        public static UIApiResponse<T> Fail(string message = "Fail")
        {
            return new UIApiResponse<T>
            {
                Status = false,
                Message = message,
                Data = default
            };
        }
    }
}
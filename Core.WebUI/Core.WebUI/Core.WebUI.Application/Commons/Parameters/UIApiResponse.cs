using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonConstructorAttribute = System.Text.Json.Serialization.JsonConstructorAttribute;

namespace Core.WebUI.Application.Commons.Parameters
{
    public class UIApiResponse<T>
    {
        public bool Status { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string RequestUri { get; set; }

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
                StatusCode = HttpStatusCode.OK,
                Message = message,
                Data = data
            };
        }

        public static UIApiResponse<T> Fail(string message = "Fail", HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            return new UIApiResponse<T>
            {
                Status = false,
                StatusCode = httpStatusCode,
                Message = message,
                Data = default
            };
        }

        public static async Task<UIApiResponse<T>> ErrorAsync(HttpResponseMessage response)
        {
            if (response.Content.Headers.ContentLength == 0)
                return Fail($"Server Error: {(int)response.StatusCode} ({response.ReasonPhrase})");

            try
            {
                SystemProblemDetails problems = await response.Content.ReadFromJsonAsync<SystemProblemDetails>();

                if (problems.Errors.Any())
                {
                    string combinedErrors = string.Join(" | ", problems.Errors.SelectMany(e => e.Value));

                    return Fail(combinedErrors);
                }

                if (!string.IsNullOrWhiteSpace(problems?.Detail))
                    return Fail(problems.Detail);

                if (!string.IsNullOrWhiteSpace(problems?.Title))
                    return Fail(problems.Title);
            }
            catch(JsonException)
            {

            }

            return new UIApiResponse<T>
            {
                Status = false,
                StatusCode = response.StatusCode,
                Message = response.ReasonPhrase,
                Data = default,
                RequestUri = response.RequestMessage.RequestUri.ToString()
            };
        }
    }
}
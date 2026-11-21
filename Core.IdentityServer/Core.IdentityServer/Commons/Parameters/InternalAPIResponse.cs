using Microsoft.IdentityModel.Tokens.Experimental;
using System.Text.Json.Serialization;

namespace Core.IdentityServer.Commons.Parameters
{
    public class InternalAPIResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public T Data { get; set; }

        public string? PagingToken { get; set; }
        public bool IsLastPage { get; set; }

        public List<string>? ValidationErrors { get; set; }
        public List<string>? BusinessRuleErrors { get; set; }

        [JsonConstructor]
        private InternalAPIResponse()
        {

        }

        public static InternalAPIResponse<T> Success(T data, string message = "Success", string? pagingToken = null, bool isLastPage = true)
        {
            return new InternalAPIResponse<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data,
                PagingToken = pagingToken,
                IsLastPage = isLastPage,
                TimeStamp = DateTimeOffset.Now,
                ValidationErrors = default,
                BusinessRuleErrors = default
            };
        }
        public static InternalAPIResponse<T> Fail(string message = "Fail", List<string>? validationErrors = null, List<string>? businessRuleErrors = null)
        {
            return new InternalAPIResponse<T>
            {
                IsSuccess = false,
                Message = message,
                Data = default,
                PagingToken = default,
                IsLastPage = default,
                TimeStamp = DateTimeOffset.Now,
                ValidationErrors = validationErrors,
                BusinessRuleErrors = businessRuleErrors
            };
        }

    }
}
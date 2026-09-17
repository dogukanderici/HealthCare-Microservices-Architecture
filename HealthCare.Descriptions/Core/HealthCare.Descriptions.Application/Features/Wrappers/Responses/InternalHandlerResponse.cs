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
    public class InternalHandlerResponse<T> : IInternalHandlerResponse, IValidationResult
    {
        public bool IsSuccess { get; set; }
        public string InternalMessage { get; set; }

        [JsonProperty]
        public T? Data { get; private set; }
        public List<string>? ValidationErrors { get; set; }

        public string? PagingToken { get; set; }
        public bool? IsLastPage { get; set; }

        [JsonConstructor]
        private InternalHandlerResponse()
        {

        }

        public static InternalHandlerResponse<T> Success(T data, string message = "Success", string? pagingToken = "", bool? isLastPage = true)
        {
            return new InternalHandlerResponse<T>
            {
                IsSuccess = true,
                InternalMessage = message,
                Data = data,
                PagingToken = pagingToken,
                IsLastPage = isLastPage
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

        public static IValidationResult WithValidationErrors(List<string> errors)
        {
            return new InternalHandlerResponse<T>
            {
                IsSuccess = false,
                InternalMessage = "Validation Error",
                ValidationErrors = errors,
                Data = default
            };
        }
    }
}
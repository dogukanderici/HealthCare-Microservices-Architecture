using Newtonsoft.Json;

namespace HealthCare.Descriptions.WebAPI.Common.Wrappers.Responses
{
    public class APIResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DateTimeOffset TimeStamp { get; private set; }
        public List<string>? Errors { get; set; }

        [JsonProperty]
        public T Data { get; private set; }

        public string PagingToken { get; set; }
        public bool IsLastPage { get; set; }

        [JsonConstructor]
        private APIResponse()
        {

        }

        public static APIResponse<T> Success<T>(T data, string message = "Success", string pagingToken = "", bool isLastPage = true)
        {
            return new APIResponse<T>
            {
                IsSuccess = true,
                Message = message,
                TimeStamp = DateTimeOffset.UtcNow,
                Data = data,
                PagingToken = pagingToken,
                IsLastPage = isLastPage,
                Errors = []
            };
        }

        public static APIResponse<T> Failure(string message = "Failure")
        {
            return new APIResponse<T>
            {
                IsSuccess = false,
                Message = message,
                TimeStamp = DateTimeOffset.UtcNow,
                Data = default,
                Errors = default
            };
        }
    }
}
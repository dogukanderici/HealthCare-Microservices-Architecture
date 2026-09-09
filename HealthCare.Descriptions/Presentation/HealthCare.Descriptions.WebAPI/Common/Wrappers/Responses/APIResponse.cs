using Newtonsoft.Json;

namespace HealthCare.Descriptions.WebAPI.Common.Wrappers.Responses
{
    public class APIResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public  DateTimeOffset TimeStamp { get; private set; }

        [JsonProperty]
        public T Data { get; private set; }

        [JsonConstructor]
        private APIResponse()
        {

        }

        public static APIResponse<T> Success<T>(T data, string message = "Success")
        {
            return new APIResponse<T>
            {
                IsSuccess = true,
                Message = message,
                TimeStamp = DateTimeOffset.UtcNow,
                Data = data
            };
        }

        public static APIResponse<T> Failure(string message = "Failure")
        {
            return new APIResponse<T>
            {
                IsSuccess = true,
                Message = message,
                TimeStamp = DateTimeOffset.UtcNow,
                Data = default
            };
        }
    }
}
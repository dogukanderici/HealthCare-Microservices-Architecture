using Core.WorkflowEngine.WebAPI.Constants;

namespace Core.WorkflowEngine.WebAPI.Wrappers
{
    public class GenericAPIResponse<T>
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public DateTimeOffset TimeStamp { get; private set; }
        public T Data { get; private set; }
        public List<string>? ValidationErrors { get; set; }
        public List<string>? BusinessRuleErrors { get; set; }

        public string PagingToken { get; set; }
        public bool? IsLastPage { get; set; }


        public static GenericAPIResponse<T> SuccessAPIResponse(T data, string? pagingToken = null, bool? isLastPage = true)
        {
            return new GenericAPIResponse<T>
            {
                IsSuccess = true,
                Message = APIConstants.Success,
                Data = data,
                TimeStamp = DateTimeOffset.UtcNow,
                PagingToken = pagingToken,
                IsLastPage = isLastPage
            };
        }

        public static GenericAPIResponse<T> ErrorAPIResponse(
            string message = APIConstants.Error,
            List<string>? validationErrors = null,
            List<string>? businessRuleErrors = null)
        {
            return new GenericAPIResponse<T>
            {
                IsSuccess = false,
                Message = message,
                Data = default!,
                TimeStamp = DateTimeOffset.UtcNow,
                ValidationErrors = validationErrors,
                BusinessRuleErrors = businessRuleErrors
            };
        }
    }
}
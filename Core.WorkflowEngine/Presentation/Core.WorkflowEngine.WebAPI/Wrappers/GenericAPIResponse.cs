using Core.WorkflowEngine.WebAPI.Constants;

namespace Core.WorkflowEngine.WebAPI.Wrappers
{
    public class GenericAPIResponse<T>
    {
        public int StatusCode { get; private set; }
        public string Message { get; private set; }
        public DateTimeOffset TimeStamp { get; private set; }
        public T Data { get; private set; }


        public static GenericAPIResponse<T> SuccessAPIResponse(T data)
        {
            return new GenericAPIResponse<T>
            {
                StatusCode = 200,
                Message = APIConstants.Success,
                Data = data,
                TimeStamp = DateTimeOffset.UtcNow
            };
        }

        public static GenericAPIResponse<T> ErrorAPIResponse()
        {
            return new GenericAPIResponse<T>
            {
                StatusCode = 400,
                Message = APIConstants.Error,
                Data = default!,
                TimeStamp = DateTimeOffset.UtcNow
            };
        }
    }
}
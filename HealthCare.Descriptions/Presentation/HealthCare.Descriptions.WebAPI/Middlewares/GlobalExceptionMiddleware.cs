using HealthCare.Descriptions.WebAPI.Wrappers;
using Serilog;

namespace HealthCare.Descriptions.WebAPI.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate requestDelegate, ILogger<GlobalExceptionMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occured!");

                int statusCode = ex switch
                {
                    InvalidDataException => StatusCodes.Status400BadRequest,
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                var responseMessage = new GenericApiResponse
                {
                    StatusCode = statusCode,
                    Message = ex.Message,
                    TimeStamp = DateTime.UtcNow
                };

                await context.Response.WriteAsJsonAsync(responseMessage);
            }
        }
    }
}

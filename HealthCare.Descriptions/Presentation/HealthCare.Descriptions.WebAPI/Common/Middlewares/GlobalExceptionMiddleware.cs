using HealthCare.Descriptions.Application.Common.CustomExceptions;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.WebAPI.Common.Wrappers.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text.Json;
using static HealthCare.Descriptions.Application.Common.Constants.ExceptionConstants;

namespace HealthCare.Descriptions.WebAPI.Common.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.GetType().Name.ToUpper()} {InvokeExMessage} {ex}");

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = ResponseContentType;

            APIResponse<bool> response = APIResponse<bool>.Failure();

            switch (ex)
            {
                case ValidationRuleException validationEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = MiddlewareMessages.ValidationMessage;
                    response.Errors = validationEx.Errors.ToList();

                    _logger.LogError($"{ex.GetType().Name.ToUpper()} {MiddlewareLogMessages.ValidationMessage}");
                    _logger.LogError(string.Join(",", validationEx.Errors));
                    break;

                case BusinessRuleException businessEx:
                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    response.Message = MiddlewareMessages.BussinessMessage;
                    response.Errors = new List<string> { businessEx.Error };

                    _logger.LogError($"{ex.GetType().Name.ToUpper()} {MiddlewareLogMessages.BussinessMessage} {ex}");
                    _logger.LogError(businessEx.Error);
                    break;

                case TokenDecryptionException tokenDecryptEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = MiddlewareMessages.TokenDecryptionMessage;

                    _logger.LogError(ex, MiddlewareLogMessages.TokenDecryptionMessage);
                    break;

                case TransactionException transactionEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = MiddlewareMessages.TransactionMessage;

                    _logger.LogError($"{ex.GetType().Name.ToUpper()} {MiddlewareLogMessages.TransactionMessage} {ex}");
                    break;

                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Message = MiddlewareMessages.UnauthorizedMessage;

                    _logger.LogError($"{ex.GetType().Name.ToUpper()} {MiddlewareLogMessages.UnauthorizedMessage}");
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = MiddlewareMessages.DefaultMessage;

                    _logger.LogError($"{ex.GetType().Name.ToUpper()} {MiddlewareLogMessages.DefaultMessage} {ex}");
                    break;
            }

            JsonSerializerSettings jsonOptions = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            string jsonResponse = JsonConvert.SerializeObject(response, jsonOptions);

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
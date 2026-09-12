using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.WebAPI.Common.Helpers.ValidationHelper;
using HealthCare.Descriptions.WebAPI.Common.Wrappers.Responses;
using Microsoft.AspNetCore.Mvc;

using static HealthCare.Descriptions.WebAPI.Common.Constants.APIResponseConstants;
using static HealthCare.Descriptions.WebAPI.Common.Constants.LogConstants;

namespace HealthCare.Descriptions.WebAPI.Common.Helpers.ControllerHelpers
{
    public class ControllerHelper<TController> : IControllerHelper<TController>
    {
        private ILogger<ControllerHelper<TController>> _logger;
        private readonly IValidationHelper<TController> _validationHelper;

        public ControllerHelper(ILogger<ControllerHelper<TController>> logger, IValidationHelper<TController> validationHelper)
        {
            _logger = logger;
            _validationHelper = validationHelper;
        }

        public async Task<IActionResult> ExecuteAsync<TData>(Func<Task<InternalHandlerResponse<TData>>> action, string actionName)
        {
            try
            {
                InternalHandlerResponse<TData> handlerResponse = await action();

                if (!handlerResponse.IsSuccess)
                {

                    if (handlerResponse.ValidationErrors.Any())
                    {
                        return _validationHelper.ExecuteValidationErrors(handlerResponse.ValidationErrors, typeof(TController).Name, actionName);
                    }

                    _logger.LogError(LogMessageTemplate,
                        typeof(TController).Name,
                        actionName);

                    return new BadRequestObjectResult(APIResponse<TData>.Failure());
                }

                _logger.LogInformation(LogMessageTemplate,
                    typeof(TController).Name,
                    actionName);

                return new OkObjectResult(APIResponse<TData>.Success(handlerResponse.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogMessageTemplate,
                    typeof(TController).Name,
                    actionName,
                    $"{ErrorMessage.CallingRequest} - {ex}");

                return new BadRequestObjectResult(APIResponse<TData>.Failure());
            }
        }
    }
}

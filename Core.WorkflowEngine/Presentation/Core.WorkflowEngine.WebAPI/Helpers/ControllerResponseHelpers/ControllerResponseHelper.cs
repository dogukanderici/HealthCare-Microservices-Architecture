using Core.WorkflowEngine.Application.Commons.CustomExceptions;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using Core.WorkflowEngine.WebAPI.Constants;
using Core.WorkflowEngine.WebAPI.Helpers.ValidationHelpers;
using Core.WorkflowEngine.WebAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;
using static Core.WorkflowEngine.WebAPI.Constants.APIConstants;
using static Core.WorkflowEngine.WebAPI.Constants.LogConstants;

namespace Core.WorkflowEngine.WebAPI.Helpers.ControllerResponseHelpers
{
    public class ControllerResponseHelper<TController> : IControllerReponseHelper<TController>
    {
        private readonly ILogger<TController> _logger;
        private readonly IValidationHelper<TController> _validationHelper;

        public ControllerResponseHelper(ILogger<TController> logger, IValidationHelper<TController> validationHelper)
        {
            _logger = logger;
            _validationHelper = validationHelper;
        }

        public async Task<IActionResult> ExecuteAsync<TData>(Func<Task<InternalHandlerResponse<TData>>> action, string actionName, string logMessage, string apiMessage)
        {
            try
            {
                InternalHandlerResponse<TData> result = await action();

                if (!result.IsSuccess)
                {
                    if (result.ValidationErrors.Any())
                    {
                        return _validationHelper.ValidationResult(
                            result.ValidationErrors,
                            typeof(TController).Name,
                            actionName);
                    }

                    _logger.LogError(LogMessageTemplate,
                                    typeof(TController).Name,
                                    actionName,
                                    logMessage);

                    return new BadRequestObjectResult(GenericAPIResponse<bool>.ErrorAPIResponse(apiMessage));
                }

                _logger.LogInformation(LogMessageTemplate,
                    typeof(TController).Name,
                    actionName,
                    SuccessMessage.CallingSuccess);

                return new OkObjectResult(GenericAPIResponse<TData>.SuccessAPIResponse(result.Data, result.PagingToken, result.IsLastPage));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogMessageTemplate,
                                    typeof(TController).Name,
                                    actionName,
                                    $"{ErrorMessage.CallingFail} - {ex}");

                switch (ex)
                {
                    case ValidationRuleException validatorEx:
                        return new BadRequestObjectResult(GenericAPIResponse<bool>.ErrorAPIResponse(APIConstants.Error, [ex.Message], []));

                    case BusinessRuleException businessEx:
                        return new BadRequestObjectResult(GenericAPIResponse<bool>.ErrorAPIResponse(APIConstants.Error, [], [ex.Message]));

                    default:
                        return new BadRequestObjectResult(GenericAPIResponse<bool>.ErrorAPIResponse(ex.Message));
                }
            }
        }
    }
}
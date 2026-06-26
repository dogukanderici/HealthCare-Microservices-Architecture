using Core.WorkflowEngine.WebAPI.Constants;
using Core.WorkflowEngine.WebAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;

using static Core.WorkflowEngine.WebAPI.Constants.LogConstants;

namespace Core.WorkflowEngine.WebAPI.Helpers.ValidationHelpers
{
    public class ValidationHelper<TRequest> : IValidationHelper<TRequest>
    {
        private readonly ILogger<TRequest> _logger;

        public ValidationHelper(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public IActionResult ValidationResult(
            List<string> validationErrors,
            string controllerName,
            string actionName
            )
        {
            _logger.LogError(LogMessageTemplate,
                                    controllerName,
                                    actionName,
                                    string.Format(ValidationTemplate, string.Join(',', validationErrors)));

            GenericAPIResponse<bool> response = GenericAPIResponse<bool>.ErrorAPIResponse(
                string.Format(APIConstants.ValidationTemplate, string.Join(',', validationErrors)));

            return new BadRequestObjectResult(response);
        }
    }
}
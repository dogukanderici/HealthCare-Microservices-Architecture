using HealthCare.Descriptions.WebAPI.Common.Wrappers.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Descriptions.WebAPI.Common.Helpers.ValidationHelper
{
    public class ValidationHelper<TController> : IValidationHelper<TController>
    {
        private readonly ILogger<TController> _logger;

        public ValidationHelper(ILogger<TController> logger)
        {
            _logger = logger;
        }

        public IActionResult ExecuteValidationErrors(List<string> errors, string controllerName, string actionName)
        {
            _logger.LogError("VALIDATION ERROR! ERRORS: {validationError}",
                string.Join(",", errors));

            APIResponse<bool> response = APIResponse<bool>.Failure(string.Join(",", errors));

            return new BadRequestObjectResult(response);
        }
    }
}
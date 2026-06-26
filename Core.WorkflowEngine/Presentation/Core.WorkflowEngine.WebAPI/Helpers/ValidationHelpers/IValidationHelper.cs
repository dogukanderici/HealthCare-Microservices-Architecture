using Microsoft.AspNetCore.Mvc;

namespace Core.WorkflowEngine.WebAPI.Helpers.ValidationHelpers
{
    public interface IValidationHelper<TRequest>
    {
        IActionResult ValidationResult(List<string> validationErrors, string controllerName, string actionName);
    }
}

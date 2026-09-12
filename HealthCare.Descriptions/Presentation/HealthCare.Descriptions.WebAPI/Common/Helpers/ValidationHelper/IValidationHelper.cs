using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Descriptions.WebAPI.Common.Helpers.ValidationHelper
{
    public interface IValidationHelper<TController>
    {
        IActionResult ExecuteValidationErrors(List<string> errors, string controllerName, string actionName);
    }
}

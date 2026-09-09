using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Descriptions.WebAPI.Common.Helpers.ControllerHelpers
{
    public interface IControllerHelper<TController>
    {
        Task<IActionResult> ExecuteAsync<TData>(Func<Task<InternalHandlerResponse<TData>>> action, string actionName);
    }
}
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace Core.WorkflowEngine.WebAPI.Helpers.ControllerResponseHelpers
{
    public interface IControllerReponseHelper<TController>
    {
        Task<IActionResult> ExecuteAsync<TData>(Func<Task<InternalHandlerResponse<TData>>> action, string actionName, string LogMessage, string APIMessage);
    }
}

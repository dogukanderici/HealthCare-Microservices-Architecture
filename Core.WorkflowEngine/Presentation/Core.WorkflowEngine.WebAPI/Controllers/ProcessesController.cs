using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessDefinitionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.WebAPI.Helpers.ControllerResponseHelpers;
using Core.WorkflowEngine.WebAPI.Helpers.ValidationHelpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Core.WorkflowEngine.WebAPI.Constants.LogConstants;

namespace Core.WorkflowEngine.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "IdentityServerAccessToken", Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IControllerReponseHelper<ProcessesController> _controllerReponseHelper;

        public ProcessesController(IMediator mediator, IControllerReponseHelper<ProcessesController> controllerReponseHelper)
        {
            _mediator = mediator;
            _controllerReponseHelper = controllerReponseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProcesses()
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(new GetProcessDefinitionsQuery()),
                nameof(GetProcesses),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProcessById(Guid id)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(new GetProcessDefinitionByIdQuery(id)),
                nameof(GetProcessById),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpPost("filtered")]
        public async Task<IActionResult> GetProcessesByFilter(GetProcessDefinitionsByFilterQuery query)
        {

            GetProcessDefinitionsByFilterQuery filter = GetProcessDefinitionsByFilterQuery.Filter(
                    query.ProcessName,
                    query.IsActive
                    );

            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(filter),
                nameof(GetProcessesByFilter),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [HttpPost]
        public async Task<IActionResult> CreateProcess(CreateProcessDefinitionCommand createProcessDefinitionCommand)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(createProcessDefinitionCommand),
                nameof(CreateProcess),
                SuccessMessage.AddingDataSuccessed,
                ErrorMessage.AddingDataFailed);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProcess(UpdateProcessDefinitionCommand updateProcessDefinitionCommand)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(updateProcessDefinitionCommand),
                nameof(UpdateProcess),
                SuccessMessage.UpdatingDataSuccessed,
                ErrorMessage.UpdatingDataFailed);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProcess(DeleteProcessDefinitionCommand deleteProcessDefinitionCommand)
        {
            return await _controllerReponseHelper.ExecuteAsync(
                () => _mediator.Send(deleteProcessDefinitionCommand),
                nameof(DeleteProcess),
                SuccessMessage.DeletingDataSuccessed,
                ErrorMessage.DeletingDataFailed);
        }
    }
}
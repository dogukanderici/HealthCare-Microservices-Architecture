using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessDefinitionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.Services;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class DeleteProcessDefinitionCommandHandler : IRequestHandler<DeleteProcessDefinitionCommand, InternalHandlerResponse<bool>>
    {

        private readonly IProcessDefinitionService _processDefinitionService;

        public DeleteProcessDefinitionCommandHandler(IProcessDefinitionService processDefinitionService)
        {
            _processDefinitionService = processDefinitionService;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(DeleteProcessDefinitionCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResult = await _processDefinitionService.DeleteAsync(request.Id, cancellationToken);

            return InternalHandlerResponse<bool>.Success(serviceResult.Data, InternalHandlerConstants.SuccessProcessDefinitionDeleting);
        }
    }
}
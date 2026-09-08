using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessDefinitionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class DeleteProcessDefinitionCommandHandler : IRequestHandler<DeleteProcessDefinitionCommand, InternalHandlerResponse<bool>>
    {

        private readonly IProcessDefinitionCommandService _processDefinitionCommandService;

        public DeleteProcessDefinitionCommandHandler(IProcessDefinitionCommandService processDefinitionCommandService)
        {
            _processDefinitionCommandService = processDefinitionCommandService;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(DeleteProcessDefinitionCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResult = await _processDefinitionCommandService.DeleteAsync(request.Id, cancellationToken);

            return InternalHandlerResponse<bool>.Success(serviceResult.Data, InternalHandlerConstants.SuccessProcessDefinitionDeleting);
        }
    }
}
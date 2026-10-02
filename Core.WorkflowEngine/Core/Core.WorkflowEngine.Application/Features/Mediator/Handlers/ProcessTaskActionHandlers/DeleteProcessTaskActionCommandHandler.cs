using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskActionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskActionServices;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskActionHandlers
{
    public class DeleteProcessTaskActionCommandHandler : IRequestHandler<DeleteProcessTaskActionCommand, InternalHandlerResponse<bool>>
    {
        private IProcessTaskActionCommandService _commandService;

        public DeleteProcessTaskActionCommandHandler(IProcessTaskActionCommandService commandService)
        {
            _commandService = commandService;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(DeleteProcessTaskActionCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResponse = await _commandService.DeleteAsync(request.Id);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
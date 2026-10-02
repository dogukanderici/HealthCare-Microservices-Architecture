using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskTransitionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.TaskTransitionServices;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskTransitionHandlers
{
    public class DeleteProcessTaskTransitionCommandHandler : IRequestHandler<DeleteProcessTaskTransitionCommand, InternalHandlerResponse<bool>>
    {
        private readonly ITaskTransitionCommandService _commandService;

        public DeleteProcessTaskTransitionCommandHandler(ITaskTransitionCommandService commandService)
        {
            _commandService = commandService;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(DeleteProcessTaskTransitionCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResponse = await _commandService.DeleteAsync(request.Id);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkItemCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers
{
    public class DeleteWorkItemCommandHandler : IRequestHandler<DeleteWorkItemCommand, InternalHandlerResponse<bool>>
    {
        private readonly IWorkItemCommandService _commandService;

        public DeleteWorkItemCommandHandler(IWorkItemCommandService commandService)
        {
            _commandService = commandService;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResponse = await _commandService.DeleteAsync(request.Id);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
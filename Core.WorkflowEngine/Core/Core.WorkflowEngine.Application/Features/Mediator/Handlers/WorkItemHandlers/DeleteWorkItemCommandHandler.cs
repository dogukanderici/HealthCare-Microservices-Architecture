using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkItemCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers
{
    public class DeleteWorkItemCommandHandler : IRequestHandler<DeleteWorkItemCommand, InternalHandlerResponse<bool>>
    {
        private readonly IWorkItemCommandService _workItemCommandService;

        public DeleteWorkItemCommandHandler(IWorkItemCommandService workItemCommandService)
        {
            _workItemCommandService = workItemCommandService;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> result = await _workItemCommandService.DeleteAsync(request.Id, cancellationToken);

            if (result.IsSuccess)
            {
                return InternalHandlerResponse<bool>.Success(true, InternalHandlerConstants.WorkItemNotFound);
            }

            return InternalHandlerResponse<bool>.Failure(InternalHandlerConstants.ErrorWorkItemDeleting);
        }
    }
}

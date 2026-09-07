using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkItemCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.Services;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers
{
    public class DeleteWorkItemCommandHandler : IRequestHandler<DeleteWorkItemCommand, InternalHandlerResponse<bool>>
    {
        private readonly IWorkItemService _workItemService;

        public DeleteWorkItemCommandHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> result = await _workItemService.DeleteAsync(request.Id, cancellationToken);

            if (result.IsSuccess)
            {
                return InternalHandlerResponse<bool>.Success(true, InternalHandlerConstants.WorkItemNotFound);
            }

            return InternalHandlerResponse<bool>.Failure(InternalHandlerConstants.ErrorWorkItemDeleting);
        }
    }
}
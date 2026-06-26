using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkItemCommands;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Configuration.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
                return InternalHandlerResponse<bool>.Success(true, InternalCommandConstants.WorkItemNotFound);
            }

            return InternalHandlerResponse<bool>.Failure(InternalCommandConstants.ErrorWorkItemDeleting);
        }
    }
}
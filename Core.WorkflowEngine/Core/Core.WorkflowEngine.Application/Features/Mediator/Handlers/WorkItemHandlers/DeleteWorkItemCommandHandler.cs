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
    public class DeleteWorkItemCommandHandler : IRequestHandler<DeleteWorkItemCommand, InternalCommandResponse<bool>>
    {
        private readonly IWorkItemService _workItemService;

        public DeleteWorkItemCommandHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        public async Task<InternalCommandResponse<bool>> Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> result = await _workItemService.DeleteAsync(request.Id, cancellationToken);

            if (result.IsSuccess)
            {
                return InternalCommandResponse<bool>.Success(true, InternalCommandConstants.WorkItemNotFound);
            }

            return InternalCommandResponse<bool>.Failure(InternalCommandConstants.ErrorWorkItemDeleting);
        }
    }
}
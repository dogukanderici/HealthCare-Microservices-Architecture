using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskTransitionCommands;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskTransitionHandlers
{
    public class DeleteProcessTaskTransitionCommandHandler : IRequestHandler<DeleteProcessTaskTransitionCommand, InternalHandlerResponse<bool>>
    {
        private readonly ITaskTransitionService _service;

        public DeleteProcessTaskTransitionCommandHandler(ITaskTransitionService service)
        {
            _service = service;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(DeleteProcessTaskTransitionCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> result = await _service.DeleteAsync(request.Id, cancellationToken);

            if (result.IsSuccess)
            {
                return InternalHandlerResponse<bool>.Success(result.Data, InternalCommandConstants.SuccessProcessTaskTransitionDeleting);
            }

            return InternalHandlerResponse<bool>.Failure(InternalCommandConstants.ErrorProcessTaskTransitionDeleting);
        }
    }
}
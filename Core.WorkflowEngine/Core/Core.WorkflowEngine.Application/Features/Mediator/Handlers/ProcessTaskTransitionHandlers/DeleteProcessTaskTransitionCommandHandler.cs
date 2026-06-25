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
    public class DeleteProcessTaskTransitionCommandHandler : IRequestHandler<DeleteProcessTaskTransitionCommand, InternalCommandResponse<bool>>
    {
        private readonly ITaskTransitionService _service;

        public DeleteProcessTaskTransitionCommandHandler(ITaskTransitionService service)
        {
            _service = service;
        }

        public async Task<InternalCommandResponse<bool>> Handle(DeleteProcessTaskTransitionCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> result = await _service.DeleteAsync(request.Id, cancellationToken);

            if (result.IsSuccess)
            {
                return InternalCommandResponse<bool>.Success(result.Data, InternalCommandConstants.SuccessProcessTaskTransitionDeleting);
            }

            return InternalCommandResponse<bool>.Failure(InternalCommandConstants.ErrorProcessTaskTransitionDeleting);
        }
    }
}
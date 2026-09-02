using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessDefinitionCommands;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Configuration.Constants;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class DeleteProcessDefinitionCommandHandler : IRequestHandler<DeleteProcessDefinitionCommand, InternalHandlerResponse<bool>>
    {

        private readonly IProcessDefinitionService _processDefinitionService;

        public DeleteProcessDefinitionCommandHandler(IProcessDefinitionService processDefinitionService)
        {
            _processDefinitionService = processDefinitionService;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(DeleteProcessDefinitionCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResult = await _processDefinitionService.DeleteAsync(request.Id, cancellationToken);

            return InternalHandlerResponse<bool>.Success(serviceResult.Data, InternalCommandConstants.SuccessProcessDefinitionDeleting);
        }
    }
}
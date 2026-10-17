using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.DynamicPolicyCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.DynamicPolicyServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.DynamicPolicyHandlers
{
    public class DeleteDynamicPolicyCommandHandler : IRequestHandler<DeleteDynamicPolicyCommand, InternalHandlerResponse<bool>>
    {
        private readonly IDynamicPolicyCommandService _commandService;

        public DeleteDynamicPolicyCommandHandler(IDynamicPolicyCommandService commandService)
        {
            _commandService = commandService;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(DeleteDynamicPolicyCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResponse = await _commandService.DeleteAsync(request.Id);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
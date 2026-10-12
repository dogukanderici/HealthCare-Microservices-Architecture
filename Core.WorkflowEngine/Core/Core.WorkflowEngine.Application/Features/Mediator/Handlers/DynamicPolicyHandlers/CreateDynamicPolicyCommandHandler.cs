using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.DynamicPolicyCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.DynamicPolicyServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.DynamicPolicyHandlers
{
    public class CreateDynamicPolicyCommandHandler : IRequestHandler<CreateDynamicPolicyCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IDynamicPolicyCommandService _commandService;
        private readonly IMapper _mapper;

        public CreateDynamicPolicyCommandHandler(IDynamicPolicyCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateDynamicPolicyCommand request, CancellationToken cancellationToken)
        {
            DynamicPolicy dataFromDto = _mapper.Map<DynamicPolicy>(request);

            InternalServiceResponse<Guid> serviceResponse = await _commandService.CreateAsync(dataFromDto, cancellationToken);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.DynamicRoleCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.DynamicRoleServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.DynamicRoleHandlers
{
    public class CreateDynamicRoleCommandHandler : IRequestHandler<CreateDynamicRoleCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IDynamicRoleCommandService _commandService;
        private readonly IMapper _mapper;

        public CreateDynamicRoleCommandHandler(IDynamicRoleCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateDynamicRoleCommand request, CancellationToken cancellationToken)
        {
            DynamicRole dataFromDto = _mapper.Map<DynamicRole>(request);

            InternalServiceResponse<Guid> serviceResponse = await _commandService.CreateAsync(dataFromDto, cancellationToken);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
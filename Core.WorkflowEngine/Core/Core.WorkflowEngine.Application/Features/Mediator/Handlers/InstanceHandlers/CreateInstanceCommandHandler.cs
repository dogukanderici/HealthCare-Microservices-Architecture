using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.InstanceCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers
{
    public class CreateInstanceCommandHandler : IRequestHandler<CreateInstanceCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IInstanceCommandService _instanceCommandService;
        private readonly IMapper _mapper;

        public CreateInstanceCommandHandler(IInstanceCommandService instanceCommandService, IMapper mapper)
        {
            _instanceCommandService = instanceCommandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateInstanceCommand request, CancellationToken cancellationToken)
        {

            Instance instanceEntity = _mapper.Map<Instance>(request);
            instanceEntity.InitiatorWorkItemId = null;

            InternalServiceResponse<Guid> serviceResponse = await _instanceCommandService.CreateAsync(instanceEntity, cancellationToken);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
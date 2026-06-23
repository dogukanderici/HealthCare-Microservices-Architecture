using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.InstanceCommands;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Configuration.Constants;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers
{
    public class CreateInstanceCommandHandler : IRequestHandler<CreateInstanceCommand, InternalCommandResponse<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateInstanceCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInstanceService _instanceService;

        public CreateInstanceCommandHandler(IMapper mapper, ILogger<CreateInstanceCommandHandler> logger, IUnitOfWork unitOfWork, IInstanceService instanceService)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _instanceService = instanceService;
        }

        public async Task<InternalCommandResponse<Guid>> Handle(CreateInstanceCommand request, CancellationToken cancellationToken)
        {

            Instance instanceEntity = _mapper.Map<Instance>(request);
            instanceEntity.InitiatorWorkItemId = null;

            InternalServiceResponse<Guid> serviceResponse = await _instanceService.CreateAsync(instanceEntity, cancellationToken);

            if (serviceResponse.IsSuccess)
            {

                return InternalCommandResponse<Guid>.Success(serviceResponse.Data, InternalCommandConstants.SuccessInstanceCreating);
            }

            return InternalCommandResponse<Guid>.Failure(InternalCommandConstants.ErrorInstanceCreating);

        }
    }
}
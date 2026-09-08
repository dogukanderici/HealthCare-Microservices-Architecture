using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Constants;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInstanceCommandService _instanceCommandService;
        private readonly ILogger<CreateInstanceCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateInstanceCommandHandler(IUnitOfWork unitOfWork, IInstanceCommandService instanceCommandService, ILogger<CreateInstanceCommandHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _instanceCommandService = instanceCommandService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateInstanceCommand request, CancellationToken cancellationToken)
        {

            Instance instanceEntity = _mapper.Map<Instance>(request);
            instanceEntity.InitiatorWorkItemId = null;

            InternalServiceResponse<Guid> serviceResponse = await _instanceCommandService.CreateAsync(instanceEntity, cancellationToken);

            if (serviceResponse.IsSuccess)
            {

                return InternalHandlerResponse<Guid>.Success(serviceResponse.Data, InternalHandlerConstants.SuccessInstanceCreating);
            }

            return InternalHandlerResponse<Guid>.Failure(InternalHandlerConstants.ErrorInstanceCreating);

        }
    }
}
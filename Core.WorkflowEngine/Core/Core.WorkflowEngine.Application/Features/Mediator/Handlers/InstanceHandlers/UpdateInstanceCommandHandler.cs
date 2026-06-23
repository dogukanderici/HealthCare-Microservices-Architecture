using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.InstanceCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.InstanceBusinessRules;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Configuration.Constants;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Abstractions;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers
{
    public class UpdateInstanceCommandHandler : IRequestHandler<UpdateInstanceCommand, InternalCommandResponse<DateTimeOffset>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateInstanceCommandHandler> _logger;
        private readonly IInstanceService _instanceService;

        public UpdateInstanceCommandHandler(IMapper mapper, ILogger<UpdateInstanceCommandHandler> logger, IInstanceService instanceService)
        {
            _mapper = mapper;
            _logger = logger;
            _instanceService = instanceService;
        }

        public async Task<InternalCommandResponse<DateTimeOffset>> Handle(UpdateInstanceCommand request, CancellationToken cancellationToken)
        {
            Instance dataFromDto = _mapper.Map<Instance>(request);

            InternalServiceResponse<DateTimeOffset> serviceResponse = await _instanceService.UpdateAsync(dataFromDto, cancellationToken);

            // Tüm business kuralları true ise güncelleme işlemini yapar.
            if (serviceResponse.IsSuccess)
            {
                _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(UpdateInstanceCommandHandler),
                        LogConstants.SuccessMessages.DataUpdatedSuccessfully);

                return InternalCommandResponse<DateTimeOffset>.Success(serviceResponse.Data, InternalCommandConstants.SuccessInstanceUpdating);
            }

            _logger.LogError(LogConstants.LogMessageTemplate,
                nameof(UpdateInstanceCommandHandler),
                serviceResponse.ServiceMessage);

            return InternalCommandResponse<DateTimeOffset>.Failure(InternalCommandConstants.InvalidBusinessRule);
        }
    }
}
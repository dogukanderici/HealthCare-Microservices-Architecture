using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.InstanceCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers
{
    public class UpdateInstanceCommandHandler : IRequestHandler<UpdateInstanceCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly IInstanceCommandService _instanceCommandService;
        private readonly ILogger<UpdateInstanceCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateInstanceCommandHandler(IInstanceCommandService instanceCommandService, ILogger<UpdateInstanceCommandHandler> logger, IMapper mapper)
        {
            _instanceCommandService = instanceCommandService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateInstanceCommand request, CancellationToken cancellationToken)
        {

            Instance existedData = await _instanceCommandService.GetWorkItemForUpdateAsync(request.Id);

            _mapper.Map(request, existedData);

            InternalServiceResponse<DateTimeOffset> serviceResponse = await _instanceCommandService.UpdateAsync(existedData, cancellationToken);

            // Tüm business kuralları true ise güncelleme işlemini yapar.
            if (serviceResponse.IsSuccess)
            {
                _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(UpdateInstanceCommandHandler),
                        LogConstants.SuccessMessages.DataUpdatedSuccessfully);

                return InternalHandlerResponse<DateTimeOffset>.Success(serviceResponse.Data, InternalHandlerConstants.SuccessInstanceUpdating);
            }

            _logger.LogError(LogConstants.LogMessageTemplate,
                nameof(UpdateInstanceCommandHandler),
                serviceResponse.ServiceMessage);

            return InternalHandlerResponse<DateTimeOffset>.Failure(InternalHandlerConstants.InvalidBusinessRule);
        }
    }
}
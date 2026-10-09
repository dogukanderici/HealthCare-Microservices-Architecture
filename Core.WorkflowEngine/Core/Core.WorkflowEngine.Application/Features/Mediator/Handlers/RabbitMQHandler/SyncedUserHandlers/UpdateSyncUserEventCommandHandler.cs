using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.RabbitMQCommands.SyncedUserCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.IntegrationServices.RabbitMQ.UserEvent;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.IntegrationServices.RabbitMQServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.RabbitMQHandler.SyncedUserHandlers
{
    public class UpdateSyncUserEventCommandHandler : IRequestHandler<UpdateSyncUserEventCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly ISyncUserEventCommandService _commandService;
        private readonly IMapper _mapper;

        public UpdateSyncUserEventCommandHandler(ISyncUserEventCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateSyncUserEventCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<SyncedUser> existedData = await _commandService.GetDataForUpdateAsync(request.Id);

            if (existedData == null)
                return InternalHandlerResponse<DateTimeOffset>.Failure();

            _mapper.Map(request, existedData);

            InternalServiceResponse<DateTimeOffset> serviceResponse = await _commandService.UpdateAsync(existedData.Data);

            if (serviceResponse.IsSuccess)
                return InternalHandlerResponse<DateTimeOffset>.Success(serviceResponse.Data);

            return InternalHandlerResponse<DateTimeOffset>.Failure();
        }
    }
}
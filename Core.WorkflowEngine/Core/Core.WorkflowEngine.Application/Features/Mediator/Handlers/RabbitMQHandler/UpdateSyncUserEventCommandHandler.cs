using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.RabbitMQCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.IntegrationServices.RabbitMQ;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.IntegrationServices.RabbitMQServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.RabbitMQHandler
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
            SyncUserEvent existedData = await _commandService.GetDataForUpdateAsync(request.Id);

            if (existedData == null)
                return InternalHandlerResponse<DateTimeOffset>.Failure();

            _mapper.Map(request, existedData);

            InternalServiceResponse<DateTimeOffset> serviceResponse = await _commandService.UpdateAsync(existedData, cancellationToken);

            if (serviceResponse.IsSuccess)
                return InternalHandlerResponse<DateTimeOffset>.Success(serviceResponse.Data);

            return InternalHandlerResponse<DateTimeOffset>.Failure();
        }
    }
}
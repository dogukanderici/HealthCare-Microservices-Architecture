using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.UserEvent.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.IntegrationServices.RabbitMQ;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Services.HandlerServices.RabbitMQ;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.UserEvent.Handlers
{
    public class SyncUserEventCreateCommandHandler : IRequestHandler<CreateSyncUserEventCommand, InternalHandlerResponse<Guid>>
    {
        private readonly ISyncUserEventCommandService _commandService;
        private readonly IMapper _mapper;

        public SyncUserEventCreateCommandHandler(ISyncUserEventCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateSyncUserEventCommand request, CancellationToken cancellationToken)
        {
            SyncUserEvent dataFromDto = _mapper.Map<SyncUserEvent>(request);

            InternalServiceResponse<Guid> serviceResponse = await _commandService.CreateAsync(dataFromDto);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
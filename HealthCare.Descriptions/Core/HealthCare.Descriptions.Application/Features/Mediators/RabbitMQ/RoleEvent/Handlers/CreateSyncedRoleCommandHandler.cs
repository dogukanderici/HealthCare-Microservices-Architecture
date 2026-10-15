using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.RoleEvent.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.IntegrationServices.RabbitMQ.SyncedRoles;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.RoleEvent.Handlers
{
    public class CreateSyncedRoleCommandHandler : IRequestHandler<CreateSyncedRoleCommand, InternalHandlerResponse<Guid>>
    {
        private readonly ISyncedRoleCommandService _commandService;
        private readonly IMapper _mapper;

        public CreateSyncedRoleCommandHandler(ISyncedRoleCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateSyncedRoleCommand request, CancellationToken cancellationToken)
        {
            SyncedRole dataFromDto = _mapper.Map<SyncedRole>(request);

            InternalServiceResponse<Guid> serviceResponse = await _commandService.CreateAsync(dataFromDto);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
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
    public class UpdateSyncedRoleCommandHandler : IRequestHandler<UpdateSyncedRoleCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly ISyncedRoleCommandService _commandService;
        private readonly IMapper _mapper;

        public UpdateSyncedRoleCommandHandler(ISyncedRoleCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }
        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateSyncedRoleCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<SyncedRole> existedData = await _commandService.GetDataForUpdateAsync(request.Id);

            if (!existedData.IsSuccess)
                return InternalHandlerResponse<DateTimeOffset>.Failure(existedData.ServiceMessage);

            _mapper.Map(request, existedData.Data);

            InternalServiceResponse<DateTimeOffset> serviceResponse = await _commandService.UpdateAsync(existedData.Data);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
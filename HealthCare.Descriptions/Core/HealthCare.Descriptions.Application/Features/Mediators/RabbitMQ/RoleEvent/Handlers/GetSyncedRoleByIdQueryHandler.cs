using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.RoleEvent.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.RoleEvent.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.IntegrationServices.RabbitMQ.SyncedRoles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.RoleEvent.Handlers
{
    public class GetSyncedRoleByIdQueryHandler : IRequestHandler<GetSyncedRoleByIdQuery, InternalHandlerResponse<GetSyncedRoleByIdQueryResult>>
    {
        private readonly ISyncedRoleQueryService _queryService;

        public GetSyncedRoleByIdQueryHandler(ISyncedRoleQueryService queryService)
        {
            _queryService = queryService;
        }
        public async Task<InternalHandlerResponse<GetSyncedRoleByIdQueryResult>> Handle(GetSyncedRoleByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetSyncedRoleByIdQueryResult> serviceResponse = await _queryService.GetDataAsync<GetSyncedRoleByIdQueryResult>(request.Id);

            return serviceResponse.ToHandlerResponse();
        }
    }
}

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
    public class GetSyncedRolesQueryHandler : IRequestHandler<GetSyncedRolesQuery, InternalHandlerResponse<IReadOnlyCollection<GetSyncedRolesQueryResult>>>
    {
        private readonly ISyncedRoleQueryService _queryService;

        public GetSyncedRolesQueryHandler(ISyncedRoleQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetSyncedRolesQueryResult>>> Handle(GetSyncedRolesQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<IReadOnlyCollection<GetSyncedRolesQueryResult>> serviceResponse =
                await _queryService.GetDatasAsync<GetSyncedRolesQueryResult>();

            return serviceResponse.ToHandlerResponse();
        }
    }
}
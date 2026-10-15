using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.DynamicRoles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Handlers
{
    public class GetDynamicRolesQueryHandler : IRequestHandler<GetDynamicRolesQuery, InternalHandlerResponse<IReadOnlyCollection<GetDynamicRolesQueryResult>>>
    {
        private readonly IDynamicRoleQueryService _queryService;

        public GetDynamicRolesQueryHandler(IDynamicRoleQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetDynamicRolesQueryResult>>> Handle(GetDynamicRolesQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<IReadOnlyCollection<GetDynamicRolesQueryResult>> serviceResponse =
                await _queryService.GetDatasAsync<GetDynamicRolesQueryResult>();

            return serviceResponse.ToHandlerResponse();
        }
    }
}
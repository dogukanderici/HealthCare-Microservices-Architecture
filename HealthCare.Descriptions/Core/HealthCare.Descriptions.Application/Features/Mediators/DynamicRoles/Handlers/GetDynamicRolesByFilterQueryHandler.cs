using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.DynamicRoles;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Handlers
{
    public class GetDynamicRolesByFilterQueryHandler : IRequestHandler<GetDynamicRolesByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetDynamicRolesByFilterQueryResult>>>
    {
        private readonly IDynamicRoleQueryService _queryService;

        public GetDynamicRolesByFilterQueryHandler(IDynamicRoleQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetDynamicRolesByFilterQueryResult>>> Handle(GetDynamicRolesByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<DynamicRole> dBQueryOptions = new DBQueryOptions<DynamicRole>();
            dBQueryOptions.filter = x => (
                (!request.DynamicPolicyId.HasValue || x.DynamicPolicyId == request.DynamicPolicyId) &&
                (!request.AllowedRoleId.HasValue || x.AllowedRoleId == request.AllowedRoleId) &&
                (string.IsNullOrEmpty(request.Methode) || x.DynamicPolicy.Methode == request.Methode) &&
                (string.IsNullOrEmpty(request.Endpoint) || x.DynamicPolicy.Endpoint == request.Endpoint) &&
                (!request.IsActive.HasValue || x.IsAvailable == request.IsActive)
            );

            dBQueryOptions.includes = [x => x.DynamicPolicy, x => x.SyncedRole];

            InternalServiceResponse<IReadOnlyCollection<GetDynamicRolesByFilterQueryResult>> serviceResponse =
                await _queryService.GetDatasAsync<GetDynamicRolesByFilterQueryResult>(dBQueryOptions);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
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
    public class GetDynamicRoleByIdQueryHandler : IRequestHandler<GetDynamicRoleByIdQuery, InternalHandlerResponse<GetDynamicRoleByIdQueryResult>>
    {
        private readonly IDynamicRoleQueryService _queryService;

        public GetDynamicRoleByIdQueryHandler(IDynamicRoleQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<GetDynamicRoleByIdQueryResult>> Handle(GetDynamicRoleByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetDynamicRoleByIdQueryResult> serviceResponse =
                await _queryService.GetDataAsync<GetDynamicRoleByIdQueryResult>(request.Id);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
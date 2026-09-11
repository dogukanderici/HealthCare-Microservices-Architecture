using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinicQuotas;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Handlers
{
    public class GetHospitalPoliclinicQuotaByIdQueryHandler : IRequestHandler<GetHospitalPoliclinicQuotaByIdQuery, InternalHandlerResponse<GetHospitalPoliclinicQuotaByIdQueryResult>>
    {
        private readonly IHospitalPoliclinicQuotaQueryService _queryService;

        public GetHospitalPoliclinicQuotaByIdQueryHandler(IHospitalPoliclinicQuotaQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<GetHospitalPoliclinicQuotaByIdQueryResult>> Handle(GetHospitalPoliclinicQuotaByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetHospitalPoliclinicQuotaByIdQueryResult> serviceResponse =
                await _queryService.GetDataAsync<GetHospitalPoliclinicQuotaByIdQueryResult>(request.Id);

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}
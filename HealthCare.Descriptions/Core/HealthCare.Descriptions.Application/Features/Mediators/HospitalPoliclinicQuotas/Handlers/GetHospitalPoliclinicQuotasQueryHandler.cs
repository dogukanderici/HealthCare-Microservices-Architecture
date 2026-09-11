using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinicQuotas;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Handlers
{
    public class GetHospitalPoliclinicQuotasQueryHandler : IRequestHandler<GetHospitalPoliclinicQuotasQuery, InternalHandlerResponse<IReadOnlyCollection<GetHospitalPoliclinicQuotasQueryResult>>>
    {
        private readonly IHospitalPoliclinicQuotaQueryService _queryService;

        public GetHospitalPoliclinicQuotasQueryHandler(IHospitalPoliclinicQuotaQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetHospitalPoliclinicQuotasQueryResult>>> Handle(GetHospitalPoliclinicQuotasQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<IReadOnlyCollection<GetHospitalPoliclinicQuotasQueryResult>> serviceResponse =
                await _queryService.GetDatasAsync<GetHospitalPoliclinicQuotasQueryResult>();

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}

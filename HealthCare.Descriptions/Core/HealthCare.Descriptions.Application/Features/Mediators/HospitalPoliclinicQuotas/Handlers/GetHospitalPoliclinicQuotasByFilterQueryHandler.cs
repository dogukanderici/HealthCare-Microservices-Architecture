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
    public class GetHospitalPoliclinicQuotasByFilterQueryHandler : IRequestHandler<GetHospitalPoliclinicQuotasByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetHospitalPoliclinicQuotasByFilterQueryResult>>>
    {
        private readonly IHospitalPoliclinicQuotaQueryService _queryService;

        public GetHospitalPoliclinicQuotasByFilterQueryHandler(IHospitalPoliclinicQuotaQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetHospitalPoliclinicQuotasByFilterQueryResult>>> Handle(GetHospitalPoliclinicQuotasByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<HospitalPoliclinicQuota> dBQueryOptions = new DBQueryOptions<HospitalPoliclinicQuota>();
            Expression<Func<HospitalPoliclinicQuota, bool>> filter = x => (
                (!request.HospitalPoliclinicId.HasValue || x.HospitalPoliclinicId == request.HospitalPoliclinicId) &&
                (!request.QuotaTypeId.HasValue || x.QuotaTypeId == request.QuotaTypeId) &&
                (!request.ValidityDate.HasValue || x.ValidityDate == request.ValidityDate) &&
                (!request.IsAvailable.HasValue || x.IsAvailable == request.IsAvailable)
            );
            dBQueryOptions.filter = filter;

            InternalServiceResponse<IReadOnlyCollection<GetHospitalPoliclinicQuotasByFilterQueryResult>> serviceResponse =
                await _queryService.GetDatasAsync<GetHospitalPoliclinicQuotasByFilterQueryResult>(dBQueryOptions);

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}
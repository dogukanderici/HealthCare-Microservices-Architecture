using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Helpers;
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
        private readonly ITokenBasedPaginationHelper<HospitalPoliclinicQuota, GetHospitalPoliclinicQuotasByFilterQueryHandler, GetHospitalPoliclinicQuotasByFilterQueryResult, string> _paginationHelper;

        public GetHospitalPoliclinicQuotasByFilterQueryHandler(IHospitalPoliclinicQuotaQueryService queryService, ITokenBasedPaginationHelper<HospitalPoliclinicQuota, GetHospitalPoliclinicQuotasByFilterQueryHandler, GetHospitalPoliclinicQuotasByFilterQueryResult, string> paginationHelper)
        {
            _queryService = queryService;
            _paginationHelper = paginationHelper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetHospitalPoliclinicQuotasByFilterQueryResult>>> Handle(GetHospitalPoliclinicQuotasByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<HospitalPoliclinicQuota> dBQueryOptions = new DBQueryOptions<HospitalPoliclinicQuota>();

            dBQueryOptions.filter = x => (
                (string.IsNullOrEmpty(request.HospitalCode) || x.HospitalPoliclinic.Hospital.HospitalCode == request.HospitalCode) &&
                (!request.HospitalPoliclinicId.HasValue || x.HospitalPoliclinicId == request.HospitalPoliclinicId) &&
                (!request.QuotaTypeId.HasValue || x.QuotaTypeId == request.QuotaTypeId) &&
                (!request.ValidityDate.HasValue || x.ValidityDate == request.ValidityDate) &&
                (!request.IsAvailable.HasValue || x.IsAvailable == request.IsAvailable)
            );
            dBQueryOptions.thenOrderBy =
                new Dictionary<Expression<Func<HospitalPoliclinicQuota, object>>, List<Expression<Func<HospitalPoliclinicQuota, object>>>>
            {
                {
                    x=>x.HospitalPoliclinic.Hospital.HospitalName,
                    new List<Expression<Func<HospitalPoliclinicQuota, object>>>
                    {
                        y=>y.HospitalPoliclinic.Hospital.City.Plate,
                        y=>y.HospitalPoliclinic.Policlinic.PoliclinicName
                    }
                }
            };
            dBQueryOptions.thenBySortingType = 0;

            var config = new TokenPayloadConfig<HospitalPoliclinicQuota, GetHospitalPoliclinicQuotasByFilterQueryHandler, GetHospitalPoliclinicQuotasByFilterQueryResult, string>
            {
                Token = request.Token,

                OrderBy = x => x.HospitalPoliclinic.Hospital.HospitalName,
                ForwardFilter = lastName => x => string.Compare(x.HospitalPoliclinic.Hospital.HospitalName, lastName) > 0,
                BackwardFilter = firstName => x => string.Compare(x.HospitalPoliclinic.Hospital.HospitalName, firstName) < 0,

                CursorSelector = x => x.HospitalPoliclinic.Hospital.HospitalName,
                CreatedAtSelector = x => x.CreatedAt,

                GetTotalCountAsync = async (options) =>
                {
                    InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(options);

                    return serviceResponse.Data;
                },

                FetchDataAsync = async (options) =>
                {
                    InternalServiceResponse<IReadOnlyCollection<GetHospitalPoliclinicQuotasByFilterQueryResult>> serviceResponse =
                        await _queryService.GetDatasAsync<GetHospitalPoliclinicQuotasByFilterQueryResult>(options);

                    return serviceResponse;
                }
            };

            return await _paginationHelper.PaginationResultAsync(config, dBQueryOptions);

            //InternalServiceResponse<IReadOnlyCollection<GetHospitalPoliclinicQuotasByFilterQueryResult>> serviceResponse =
            //    await _queryService.GetDatasAsync<GetHospitalPoliclinicQuotasByFilterQueryResult>(dBQueryOptions);

            //return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}
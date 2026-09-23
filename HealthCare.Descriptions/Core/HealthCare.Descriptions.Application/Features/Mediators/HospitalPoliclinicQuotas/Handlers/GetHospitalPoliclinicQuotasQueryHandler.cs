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
    public class GetHospitalPoliclinicQuotasQueryHandler : IRequestHandler<GetHospitalPoliclinicQuotasQuery, InternalHandlerResponse<IReadOnlyCollection<GetHospitalPoliclinicQuotasQueryResult>>>
    {
        private readonly IHospitalPoliclinicQuotaQueryService _queryService;
        private readonly ITokenBasedPaginationHelper<HospitalPoliclinicQuota, GetHospitalPoliclinicQuotasQueryHandler, GetHospitalPoliclinicQuotasQueryResult, string> _paginationHelper;

        public GetHospitalPoliclinicQuotasQueryHandler(IHospitalPoliclinicQuotaQueryService queryService, ITokenBasedPaginationHelper<HospitalPoliclinicQuota, GetHospitalPoliclinicQuotasQueryHandler, GetHospitalPoliclinicQuotasQueryResult, string> paginationHelper)
        {
            _queryService = queryService;
            _paginationHelper = paginationHelper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetHospitalPoliclinicQuotasQueryResult>>> Handle(GetHospitalPoliclinicQuotasQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<HospitalPoliclinicQuota> dBQueryOptions = new DBQueryOptions<HospitalPoliclinicQuota>();

            dBQueryOptions.filter = x => (
                (x.HospitalPoliclinic.Hospital.HospitalCode == request.HospitalCode) &&
                (x.HospitalPoliclinic.Hospital.IsAvailable == true)
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

            var config = new TokenPayloadConfig<HospitalPoliclinicQuota, GetHospitalPoliclinicQuotasQueryHandler, GetHospitalPoliclinicQuotasQueryResult, string>
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
                    InternalServiceResponse<IReadOnlyCollection<GetHospitalPoliclinicQuotasQueryResult>> serviceResponse =
                        await _queryService.GetDatasAsync<GetHospitalPoliclinicQuotasQueryResult>(options);

                    return serviceResponse;
                }
            };

            return await _paginationHelper.PaginationResultAsync(config, dBQueryOptions);

            //InternalServiceResponse<IReadOnlyCollection<GetHospitalPoliclinicQuotasQueryResult>> serviceResponse =
            //    await _queryService.GetDatasAsync<GetHospitalPoliclinicQuotasQueryResult>();

            //return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}

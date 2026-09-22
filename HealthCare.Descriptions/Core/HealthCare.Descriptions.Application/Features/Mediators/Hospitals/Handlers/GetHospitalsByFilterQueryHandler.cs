using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Handlers.Extensions.QueryHandlerExtension;
using HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Helpers;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Hospitals;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Handlers
{
    public class GetHospitalsByFilterQueryHandler : IRequestHandler<GetHospitalsByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetHospitalsByFilterQueryResult>>>
    {
        private readonly IHospitalQueryService _queryService;
        private readonly ITokenBasedPaginationHelper<Hospital, GetHospitalsByFilterQueryHandler, GetHospitalsByFilterQueryResult, string> _paginationHelper;

        public GetHospitalsByFilterQueryHandler(IHospitalQueryService queryService, ITokenBasedPaginationHelper<Hospital, GetHospitalsByFilterQueryHandler, GetHospitalsByFilterQueryResult, string> paginationHelper)
        {
            _queryService = queryService;
            _paginationHelper = paginationHelper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetHospitalsByFilterQueryResult>>> Handle(GetHospitalsByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<Hospital> dBQueryOptions = new DBQueryOptions<Hospital>();

            Expression<Func<Hospital, bool>> filter = x => (
                (!request.CityId.HasValue || x.HospitalCity == request.CityId) &&
                (!request.DistrictId.HasValue || x.HospitalDistrict == request.DistrictId) &&
                (string.IsNullOrEmpty(request.Code) || x.HospitalCode == request.Code) &&
                (!request.IsActive.HasValue || x.IsAvailable == request.IsActive)
            );

            dBQueryOptions.filter = filter;

            dBQueryOptions.thenOrderBy = new Dictionary<Expression<Func<Hospital, object>>, List<Expression<Func<Hospital, object>>>>
            {
                {
                    x=>x.HospitalName,
                    new List<Expression<Func<Hospital, object>>>
                    {
                        y=>y.City.Plate,
                        y=>y.District.DistrictName
                    }
                }
            };

            dBQueryOptions.thenBySortingType = 0;

            // Hastane verilerine il-ilçe bilgisinin gelmesi için ilişki ekler.
            dBQueryOptions = HospitalQueryOptionExtension.AddCityWithDistrict(dBQueryOptions);

            var config = new TokenPayloadConfig<Hospital, GetHospitalsByFilterQueryHandler, GetHospitalsByFilterQueryResult, string>
            {
                Token = request.Token,

                OrderBy = x => x.HospitalName,
                ForwardFilter = lastName => x => string.Compare(x.HospitalName, lastName) > 0,
                BackwardFilter = firstName => x => string.Compare(x.HospitalName, firstName) < 0,

                CursorSelector = X => X.HospitalName,
                CreatedAtSelector = x => x.CreatedAt,

                GetTotalCountAsync = async () =>
                {
                    InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync();
                    return serviceResponse.Data;
                },
                FetchDataAsync = async (options) =>
                {
                    InternalServiceResponse<IReadOnlyCollection<GetHospitalsByFilterQueryResult>> serviceResponse =
                        await _queryService.GetDatasAsync<GetHospitalsByFilterQueryResult>(options);

                    return serviceResponse;
                }
            };

            return await _paginationHelper.PaginationResultAsync(config, dBQueryOptions);

            //InternalServiceResponse<IReadOnlyCollection<GetHospitalsByFilterQueryResult>> serviceResult = await _queryService.GetDatasAsync<GetHospitalsByFilterQueryResult>(dBQueryOptions);

            //return ServiceResponseExtension.ToHandlerResponse(serviceResult);
        }
    }
}
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
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Handlers
{
    public class GetHospitalsQueryHandler : IRequestHandler<GetHospitalsQuery, InternalHandlerResponse<IReadOnlyCollection<GetHospitalsQueryResult>>>
    {
        private readonly IHospitalQueryService _queryService;
        private readonly ITokenBasedPaginationHelper<Hospital, GetHospitalsQueryHandler, GetHospitalsQueryResult, string> _paginationHelper;

        public GetHospitalsQueryHandler(IHospitalQueryService queryService, ITokenBasedPaginationHelper<Hospital, GetHospitalsQueryHandler, GetHospitalsQueryResult, string> paginationHelper)
        {
            _queryService = queryService;
            _paginationHelper = paginationHelper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetHospitalsQueryResult>>> Handle(GetHospitalsQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<Hospital> dBQueryOptions = new DBQueryOptions<Hospital>();

            // Hastane verilerine il-ilçe bilgisinin gelmesi için ilişki ekler.
            dBQueryOptions = HospitalQueryOptionExtension.AddCityWithDistrict(dBQueryOptions);

            dBQueryOptions.thenOrderBy = new Dictionary<Expression<Func<Hospital, object>>, List<Expression<Func<Hospital, object>>>>
            {
                {
                    x=>x.HospitalName,
                    new List<Expression<Func<Hospital, object>>>
                    {
                        x=>x.Id
                    }
                }
            };
            dBQueryOptions.thenBySortingType = 0;


            var config = new TokenPayloadConfig<Hospital, GetHospitalsQueryHandler, GetHospitalsQueryResult, string>
            {
                Token = request.Token,

                OrderBy = x => x.HospitalName,
                ForwardFilter = lastName => x => string.Compare(x.HospitalName, lastName) > 0,
                BackwardFilter = firstName => x => string.Compare(x.HospitalName, firstName) < 0,

                CursorSelector = x => x.HospitalName,
                CreatedAtSelector = x => x.CreatedAt,

                GetTotalCountAsync = async () =>
                {
                    InternalServiceResponse<int> serviceResult = await _queryService.GetDataCountAsync();
                    return serviceResult.Data;
                },
                FetchDataAsync = async (options) =>
                {
                    InternalServiceResponse<IReadOnlyCollection<GetHospitalsQueryResult>> serviceResult =
                        await _queryService.GetDatasAsync<GetHospitalsQueryResult>(options);

                    return serviceResult;
                }
            };

            return await _paginationHelper.PaginationResultAsync(config, dBQueryOptions);

            //InternalServiceResponse<IReadOnlyCollection<GetHospitalsQueryResult>> serviceResult = await _queryService.GetDatasAsync<GetHospitalsQueryResult>(dBQueryOptions);

            //return ServiceResponseExtension.ToHandlerResponse(serviceResult);
        }
    }
}
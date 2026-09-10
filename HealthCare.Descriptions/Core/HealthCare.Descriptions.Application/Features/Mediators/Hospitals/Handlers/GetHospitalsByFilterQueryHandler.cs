using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Handlers.Extensions.QueryHandlerExtension;
using HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Results;
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

        public GetHospitalsByFilterQueryHandler(IHospitalQueryService queryService)
        {
            _queryService = queryService;
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

            dBQueryOptions = HospitalQueryOptionExtension.AddCityWithDistrict(dBQueryOptions);

            InternalServiceResponse<IReadOnlyCollection<GetHospitalsByFilterQueryResult>> serviceResult = await _queryService.GetDatasAsync<GetHospitalsByFilterQueryResult>(dBQueryOptions);

            return ServiceResponseExtension.ToHandlerResponse(serviceResult);
        }
    }
}
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

        public GetHospitalsQueryHandler(IHospitalQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetHospitalsQueryResult>>> Handle(GetHospitalsQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<Hospital> dBQueryOptions = new DBQueryOptions<Hospital>();

            dBQueryOptions = HospitalQueryOptionExtension.AddCityWithDistrict(dBQueryOptions);

            InternalServiceResponse<IReadOnlyCollection<GetHospitalsQueryResult>> serviceResult = await _queryService.GetDatasAsync<GetHospitalsQueryResult>(dBQueryOptions);

            return ServiceResponseExtension.ToHandlerResponse(serviceResult);
        }
    }
}
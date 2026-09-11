using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Handlers
{
    public class GetHospitalPoliclinicByIdQueryHandler : IRequestHandler<GetHospitalPoliclinicByIdQuery, InternalHandlerResponse<GetHospitalPoliclinicByIdQueryResult>>
    {
        private readonly IHospitalPoliclinicQueryService _queryService;

        public GetHospitalPoliclinicByIdQueryHandler(IHospitalPoliclinicQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<GetHospitalPoliclinicByIdQueryResult>> Handle(GetHospitalPoliclinicByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetHospitalPoliclinicByIdQueryResult> serviceResponse =
               await _queryService.GetDataAsync<GetHospitalPoliclinicByIdQueryResult>(request.Id);

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}
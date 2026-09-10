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
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Handlers
{
    public class GetHospitalByIdQueryHandler : IRequestHandler<GetHospitalByIdQuery, InternalHandlerResponse<GetHospitalByIdQueryResult>>
    {
        private readonly IHospitalQueryService _queryService;

        public GetHospitalByIdQueryHandler(IHospitalQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<GetHospitalByIdQueryResult>> Handle(GetHospitalByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetHospitalByIdQueryResult> serviceResponse = await _queryService.GetDataAsync<GetHospitalByIdQueryResult>(request.Id);

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}
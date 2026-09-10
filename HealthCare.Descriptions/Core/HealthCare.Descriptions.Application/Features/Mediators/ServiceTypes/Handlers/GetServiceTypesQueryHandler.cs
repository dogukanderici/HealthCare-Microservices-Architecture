using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.ServiceType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Handlers
{
    public class GetServiceTypesQueryHandler : IRequestHandler<GetServiceTypesQuery, InternalHandlerResponse<IReadOnlyCollection<GetServiceTypesQueryResult>>>
    {
        private readonly IServiceTypeQueryService _queryService;

        public GetServiceTypesQueryHandler(IServiceTypeQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetServiceTypesQueryResult>>> Handle(GetServiceTypesQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<IReadOnlyCollection<GetServiceTypesQueryResult>> serviceResult = await _queryService.GetDatasAsync<GetServiceTypesQueryResult>();

            return ServiceResponseExtension.ToHandlerResponse(serviceResult);
        }
    }
}
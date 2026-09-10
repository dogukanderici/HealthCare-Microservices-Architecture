using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Policlinics;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Handlers
{
    public class GetPoliclinicsByFilterQueryHandler : IRequestHandler<GetPoliclinicsByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetPoliclinicsByFilterQueryResult>>>
    {
        private readonly IPoliclinicQueryService _queryService;

        public GetPoliclinicsByFilterQueryHandler(IPoliclinicQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetPoliclinicsByFilterQueryResult>>> Handle(GetPoliclinicsByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<Policlinic> dBQueryOptions = new DBQueryOptions<Policlinic>();
            Expression<Func<Policlinic, bool>> filter = x => (
                (string.IsNullOrEmpty(request.Code) || x.PoliclinicCode == request.Code) &&
                (!request.IsActive.HasValue || x.IsAvailable == request.IsActive)
            );
            dBQueryOptions.filter = filter;

            InternalServiceResponse<IReadOnlyCollection<GetPoliclinicsByFilterQueryResult>> serviceResponse = await _queryService.GetDatasAsync<GetPoliclinicsByFilterQueryResult>(dBQueryOptions);

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}

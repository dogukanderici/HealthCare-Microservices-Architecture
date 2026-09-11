using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinics;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Handlers
{
    public class GetHospitalPoliclinicsByFilterQueryHandler : IRequestHandler<GetHospitalPoliclinicsByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetHospitalPoliclinicsByFilterQueryResult>>>
    {
        private readonly IHospitalPoliclinicQueryService _queryService;

        public GetHospitalPoliclinicsByFilterQueryHandler(IHospitalPoliclinicQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetHospitalPoliclinicsByFilterQueryResult>>> Handle(GetHospitalPoliclinicsByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<HospitalPoliclinic> dBQueryOptions = new DBQueryOptions<HospitalPoliclinic>();
            Expression<Func<HospitalPoliclinic, bool>> filter = x => (
                (!request.HospitalId.HasValue || x.HospitalId == request.HospitalId) &&
                (!request.PoliclinicId.HasValue || x.PoliclinicId == request.PoliclinicId)
            );
            dBQueryOptions.filter = filter;
            dBQueryOptions.thenIncludes = new Dictionary<Expression<Func<HospitalPoliclinic, object>>, List<Expression<Func<object, object>>>>
                {
                    {
                        x=>x.Hospital,
                        new List<Expression<Func<object, object>>>{ }
                    },

                    {
                        x=>x.Policlinic,
                        new List<Expression<Func<object, object>>>{ }
                    }
                };

            InternalServiceResponse<IReadOnlyCollection<GetHospitalPoliclinicsByFilterQueryResult>> serviceResponse =
                await _queryService.GetDatasAsync<GetHospitalPoliclinicsByFilterQueryResult>(dBQueryOptions);

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}
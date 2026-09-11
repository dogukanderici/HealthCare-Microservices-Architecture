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
    public class GetHospitalPoliclinicsQueryHandler : IRequestHandler<GetHospitalPoliclinicsQuery, InternalHandlerResponse<IReadOnlyCollection<GetHospitalPoliclinicsQueryResult>>>
    {
        private readonly IHospitalPoliclinicQueryService _queryService;

        public GetHospitalPoliclinicsQueryHandler(IHospitalPoliclinicQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetHospitalPoliclinicsQueryResult>>> Handle(GetHospitalPoliclinicsQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<HospitalPoliclinic> dBQueryOptions = new DBQueryOptions<HospitalPoliclinic>();

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

            InternalServiceResponse<IReadOnlyCollection<GetHospitalPoliclinicsQueryResult>> serviceResponse =
                await _queryService.GetDatasAsync<GetHospitalPoliclinicsQueryResult>(dBQueryOptions);

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}
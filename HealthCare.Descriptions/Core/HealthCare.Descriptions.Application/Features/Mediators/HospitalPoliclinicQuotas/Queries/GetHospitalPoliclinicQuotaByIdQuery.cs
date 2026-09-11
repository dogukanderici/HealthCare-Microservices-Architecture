using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Queries
{
    public class GetHospitalPoliclinicQuotaByIdQuery : IRequest<InternalHandlerResponse<GetHospitalPoliclinicQuotaByIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetHospitalPoliclinicQuotaByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
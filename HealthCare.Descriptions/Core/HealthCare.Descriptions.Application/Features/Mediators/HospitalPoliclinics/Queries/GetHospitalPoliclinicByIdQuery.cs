using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Queries
{
    public class GetHospitalPoliclinicByIdQuery : IRequest<InternalHandlerResponse<GetHospitalPoliclinicByIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetHospitalPoliclinicByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
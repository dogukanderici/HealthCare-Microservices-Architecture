using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalPoliclinicResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.HospitalPoliclinicQueries
{
    public class GetHospitalPoliclinicByIdQuery : IRequest<GetHospitalPoliclinicByIdQueryResult>
    {
        public Guid Id { get; set; }

        public GetHospitalPoliclinicByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

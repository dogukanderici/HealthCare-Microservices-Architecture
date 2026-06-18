using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalPoliclinicResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.HospitalPoliclinicQueries
{
    public class GetHospitalPoliclinicsByFilterQuery : IRequest<List<GetHospitalPoliclinicsByFilterQueryResult>>
    {
        public Guid? HospitalId { get; set; }
        public Guid? PoliclinicId { get; set; }
        public bool? IsAvailable { get; set; }

        private GetHospitalPoliclinicsByFilterQuery() { }

        public static GetHospitalPoliclinicsByFilterQuery Filter(Guid? hospitalId, Guid? policlinicId, bool? ısAvailable) =>
            new GetHospitalPoliclinicsByFilterQuery
            {
                HospitalId = hospitalId,
                PoliclinicId = policlinicId,
                IsAvailable = ısAvailable
            };
    }
}

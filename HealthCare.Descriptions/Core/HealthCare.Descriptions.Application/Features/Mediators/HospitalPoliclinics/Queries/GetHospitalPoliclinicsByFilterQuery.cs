using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Queries
{
    public class GetHospitalPoliclinicsByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetHospitalPoliclinicsByFilterQueryResult>>>
    {
        public Guid? HospitalId { get; set; }
        public Guid? PoliclinicId { get; set; }

        [JsonConstructor]
        private GetHospitalPoliclinicsByFilterQuery()
        {

        }

        public static GetHospitalPoliclinicsByFilterQuery Filter(Guid? hospitalId, Guid? policlinicId) =>
            new GetHospitalPoliclinicsByFilterQuery
            {
                HospitalId = hospitalId,
                PoliclinicId = policlinicId
            };
    }
}

using HealthCare.Descriptions.Application.Features.Mediator.Results.GenericResults;
using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalResults;
using HealthCare.Descriptions.Application.Features.Mediator.Results.PoliclinicResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalPoliclinicResults
{
    public class GetHospitalPoliclinicsQueryResult : GenericAuditResult
    {
        public Guid Id { get; set; }
        public Guid HospitalId { get; set; }
        public Guid PoliclinicId { get; set; }
        public bool IsAvailable { get; set; }

        public GetHospitalForHospitalPoliclinicQueryResult Hospital { get; set; }
        public GetPoliclinicForHospitalPoliclinicQueryResult Policlinic { get; set; }
    }
}

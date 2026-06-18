using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalResults;
using HealthCare.Descriptions.Application.Features.Mediator.Results.PoliclinicResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalPoliclinicResults
{
    public class GetHospitalPoliclinicForHospitalPoliclinicQuotaQueryResult
    {
        public Guid HospitalId { get; set; }
        public Guid PoliclinicId { get; set; }

        public GetHospitalForHospitalPoliclinicQuotaQueryResult Hospital { get; set; }
        public GetPoliclinicForHospitalPoliclinicQuotaQueryResult Policlinic { get; set; }
    }
}

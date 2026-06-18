using HealthCare.Descriptions.Application.Features.Mediator.Results.GenericResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalPoliclinicResults
{
    public class GetHospitalPoliclinicByIdQueryResult : GenericAuditResult
    {
        public Guid Id { get; set; }
        public Guid HospitalId { get; set; }
        public Guid PoliclinicId { get; set; }
        public bool IsAvailable { get; set; }
    }
}

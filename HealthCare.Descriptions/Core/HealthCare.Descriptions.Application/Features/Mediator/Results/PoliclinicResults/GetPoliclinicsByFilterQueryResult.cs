using HealthCare.Descriptions.Application.Features.Mediator.Results.GenericResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Results.PoliclinicResults
{
    public class GetPoliclinicsByFilterQueryResult : GenericAuditResult
    {
        public Guid Id { get; set; }
        public int PoliclinicCode { get; set; }
        public string PoliclinicName { get; set; }
        public bool IsAvailable { get; set; }
    }
}

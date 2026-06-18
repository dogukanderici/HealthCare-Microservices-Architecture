using HealthCare.Descriptions.Application.Features.Mediator.Results.GenericResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Results.DistrictResults
{
    public class GetDistrictsByFilterQueryResult : GenericAuditResult
    {
        public Guid Id { get; set; }
        public int Plate { get; set; }
        public string DistrictName { get; set; }
        public bool IsAvailable { get; set; }
    }
}

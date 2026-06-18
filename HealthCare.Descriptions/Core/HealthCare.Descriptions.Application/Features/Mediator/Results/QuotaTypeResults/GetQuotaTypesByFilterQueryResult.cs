using HealthCare.Descriptions.Application.Features.Mediator.Results.GenericResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Results.QuotaTypeResults
{
    public class GetQuotaTypesByFilterQueryResult : GenericAuditResult
    {
        public Guid Id { get; set; }
        public string QuotaTypeName { get; set; }
        public bool IsAvailable { get; set; }
    }
}

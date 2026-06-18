using HealthCare.Descriptions.Application.Features.Mediator.Results.GenericResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Results.ServiceResults
{
    public class GetServiceTypeByIdQueryResult : GenericAuditResult
    {
        public Guid Id { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public bool IsAvailable { get; set; }
    }
}

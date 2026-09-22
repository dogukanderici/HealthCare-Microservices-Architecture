using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Results
{
    public class GetPoliclinicsByFilterQueryResult : IListResult, IGenericAuditResult
    {
        public Guid Id { get; set; }
        public string PoliclinicCode { get; set; }
        public string PoliclinicName { get; set; }
        public bool IsAvailable { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Results.GenericResults
{
    public class GenericAuditResult
    {
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}

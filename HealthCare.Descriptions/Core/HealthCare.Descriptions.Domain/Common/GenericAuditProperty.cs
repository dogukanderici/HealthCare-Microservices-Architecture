using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Domain.Common
{
    public class GenericAuditProperty
    {
        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}

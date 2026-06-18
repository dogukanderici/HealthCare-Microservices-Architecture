using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Domain.Common
{
    public class GenericAuditProperty
    {
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public Guid CreatedBy { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
        public Guid UpdatedBy { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
    }
}
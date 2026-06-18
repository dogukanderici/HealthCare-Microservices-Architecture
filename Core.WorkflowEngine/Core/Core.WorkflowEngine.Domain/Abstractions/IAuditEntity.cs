using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Domain.Abstractions
{
    public interface IAuditEntity
    {
        DateTimeOffset CreatedAt { get; set; }
        Guid CreatedBy { get; set; }
        DateTimeOffset UpdatedAt { get; set; }
        Guid UpdatedBy { get; set; }
    }
}

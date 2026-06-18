using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Domain.Abstractions
{
    public interface IEntity : IAuditEntity
    {
        Guid Id { get; set; }
    }
}

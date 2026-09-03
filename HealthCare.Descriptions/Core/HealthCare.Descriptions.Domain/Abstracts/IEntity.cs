using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Domain.Abstracts
{
    public interface IEntity : IAuditProperty
    {
        public Guid Id { get; set; }
        public bool IsAvailable { get; set; }
    }
}

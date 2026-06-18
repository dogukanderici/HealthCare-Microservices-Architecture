using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Commands.GenericCommands
{
    public class GenericAuditCommand
    {
        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}

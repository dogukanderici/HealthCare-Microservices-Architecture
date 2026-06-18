using HealthCare.Descriptions.Application.Features.Mediator.Commands.GenericCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Commands.ServiceCommands
{
    public class UpdateServiceTypeCommand : GenericAuditCommand, IRequest
    {
        public Guid Id { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public bool IsAvailable { get; set; }
    }
}

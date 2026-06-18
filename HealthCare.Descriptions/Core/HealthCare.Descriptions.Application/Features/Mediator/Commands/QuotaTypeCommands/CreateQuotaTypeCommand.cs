using HealthCare.Descriptions.Application.Features.Mediator.Commands.GenericCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Commands.QuotaTypeCommands
{
    public class CreateQuotaTypeCommand : GenericAuditCommand, IRequest
    {
        public string QuotaTypeName { get; set; }
        public bool IsAvailable { get; set; }
    }
}

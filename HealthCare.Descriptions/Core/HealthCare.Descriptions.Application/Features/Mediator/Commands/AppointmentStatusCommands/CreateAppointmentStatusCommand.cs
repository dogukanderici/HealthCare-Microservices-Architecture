using HealthCare.Descriptions.Application.Features.Mediator.Commands.GenericCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Commands.AppointmentStatusCommands
{
    public class CreateAppointmentStatusCommand : GenericAuditCommand, IRequest
    {
        public string StatusName { get; set; }
        public bool IsAvailable { get; set; }
    }
}

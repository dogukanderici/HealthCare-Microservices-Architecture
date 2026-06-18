using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Commands.AppointmentStatusCommands
{
    public class DeleteAppointmentStatusCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteAppointmentStatusCommand(Guid id)
        {
            Id = id;
        }
    }
}

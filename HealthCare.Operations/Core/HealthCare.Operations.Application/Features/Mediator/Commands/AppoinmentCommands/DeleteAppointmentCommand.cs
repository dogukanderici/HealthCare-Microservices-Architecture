using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Commands.AppoinmentCommands
{
    public class DeleteAppointmentCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteAppointmentCommand(Guid id)
        {
            Id = id;
        }
    }
}

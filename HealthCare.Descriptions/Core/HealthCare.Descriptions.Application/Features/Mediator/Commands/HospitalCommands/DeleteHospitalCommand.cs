using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalCommands
{
    public class DeleteHospitalCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteHospitalCommand(Guid id)
        {
            Id = id;
        }
    }
}

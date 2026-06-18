using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalPoliclinicCommands
{
    public class DeleteHospitalPoliclinicCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteHospitalPoliclinicCommand(Guid id)
        {
            Id = id;
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalPoliclinicQuotaCommands
{
    public class DeleteHospitalPoliclinicQuotaCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteHospitalPoliclinicQuotaCommand(Guid id)
        {
            Id = id;
        }
    }
}

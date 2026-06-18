using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Commands.PoliclinicCommands
{
    public class DeletePoliclinicCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeletePoliclinicCommand(Guid id)
        {
            Id = id;
        }
    }
}

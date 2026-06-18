using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Commands.DistrictCommands
{
    public class DeleteDistrictCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteDistrictCommand(Guid id)
        {
            Id = id;
        }
    }
}

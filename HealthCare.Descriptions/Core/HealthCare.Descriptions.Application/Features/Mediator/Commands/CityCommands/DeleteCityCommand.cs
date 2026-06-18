using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Commands.CityCommands
{
    public class DeleteCityCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteCityCommand(Guid id)
        {
            Id = id;
        }
    }
}

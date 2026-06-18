using HealthCare.Descriptions.Application.Features.Mediator.Commands.GenericCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Commands.CityCommands
{
    public class CreateCityCommand : GenericAuditCommand, IRequest
    {
        public int Plate { get; set; }
        public string CityName { get; set; }
        public bool IsAvailable { get; set; }
    }
}

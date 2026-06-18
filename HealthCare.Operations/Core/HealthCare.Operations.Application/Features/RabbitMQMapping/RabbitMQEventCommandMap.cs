using HealthCare.Operations.Application.Features.Mediator.Commands.HospitalProjectionCommands;
using HealthCare.Operations.Application.Features.Mediator.Commands.PoliclinicProjectionCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.RabbitMQMapping
{
    public class RabbitMQEventCommandMap
    {
        public static readonly Dictionary<string, Type> EventMap = new()
        {
            {"CreateHospitalProjectionCommand",typeof(CreateHospitalProjectionCommand) },
            {"UpdateHospitalProjectionCommand",typeof(UpdateHospitalProjectionCommand) },
            {"CreatePoliclinicProjectionCommand",typeof(CreatePoliclinicProjectionCommand) },
            {"UpdatePoliclinicProjectionCommand",typeof(UpdatePoliclinicProjectionCommand) }

            // Diğer Command class'ları buraya eklenecek.
        };
    }
}

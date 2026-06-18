using HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.PoliclinicCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.RabbitMQMapping
{
    public class RabbitMQEventCommandMap
    {
        public static readonly Dictionary<string, Type> EventMap = new()
        {
            {"CreateHospitalCommand",typeof(CreateHospitalCommand) },
            {"UpdateHospitalCommand",typeof(UpdateHospitalCommand) },
            {"UpdatePoliclinicCommand",typeof(CreatePoliclinicCommand) },
            {"UpdatePoliclinicCommand",typeof(UpdatePoliclinicCommand) }

            // Diğer Command class'ları buraya eklenecek.
        };
    }
}

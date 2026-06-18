using HealthCare.Descriptions.Application.Features.Mediator.Commands.GenericCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Commands.PoliclinicCommands
{
    public class CreatePoliclinicCommand : GenericAuditCommand, IRequest
    {
        public int PoliclinicCode { get; set; }
        public string PoliclinicName { get; set; }
        public bool IsAvailable { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Commands.PoliclinicProjectionCommands
{
    public class UpdatePoliclinicProjectionCommand
    {
        public Guid Id { get; set; }
        public int PoliclinicCode { get; set; }
        public string PoliclinicName { get; set; }
    }
}

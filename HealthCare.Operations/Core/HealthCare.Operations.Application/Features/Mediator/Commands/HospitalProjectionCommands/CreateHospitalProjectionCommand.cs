using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Commands.HospitalProjectionCommands
{
    public class CreateHospitalProjectionCommand
    {
        public Guid Id { get; set; }
        public int HospitalCode { get; set; }
        public string HospitalName { get; set; }
        public int HospitalCity { get; set; }
        public Guid HospitalDistrict { get; set; }
    }
}

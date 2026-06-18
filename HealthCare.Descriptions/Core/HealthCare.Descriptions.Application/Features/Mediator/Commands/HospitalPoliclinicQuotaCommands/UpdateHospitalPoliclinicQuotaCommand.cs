using HealthCare.Descriptions.Application.Features.Mediator.Commands.GenericCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalPoliclinicQuotaCommands
{
    public class UpdateHospitalPoliclinicQuotaCommand : GenericAuditCommand, IRequest
    {
        public Guid Id { get; set; }
        public Guid HospitalPoliclinicId { get; set; }
        public Guid QuotaType { get; set; }
        public int Quota { get; set; }
        public DateTimeOffset ValidityDate { get; set; }
        public bool IsAvailable { get; set; }
    }
}

using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Commands
{
    public class CreateHospitalPoliclinicCommand : IRequest<InternalHandlerResponse<Guid>>, ITransactionalRequest
    {
        public Guid HospitalId { get; set; }
        public Guid PoliclinicId { get; set; }
        public bool IsAvailable { get; set; }
    }
}

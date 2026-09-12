using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Commands
{
    public class UpdateHospitalCommand : IRequest<InternalHandlerResponse<DateTimeOffset>>, ITransactionalRequest, IValidationRequest
    {
        public Guid Id { get; set; }
        public string HospitalCode { get; set; }
        public string HospitalName { get; set; }
        public bool IsAvailable { get; set; }
    }
}
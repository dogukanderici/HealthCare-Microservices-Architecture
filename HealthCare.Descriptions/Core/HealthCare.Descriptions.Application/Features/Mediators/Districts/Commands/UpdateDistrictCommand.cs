using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Districts.Commands
{
    public class UpdateDistrictCommand : IRequest<InternalHandlerResponse<DateTimeOffset>>, ITransactionalRequest, IValidationRequest
    {
        public Guid Id { get; set; }
        public string DistrictName { get; set; }
    }
}
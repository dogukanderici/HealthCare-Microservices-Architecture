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
    public class CreateDistrictCommand : IRequest<InternalHandlerResponse<Guid>>, ITransactionalRequest, IValidationRequest
    {
        public Guid CityId { get; set; }
        public int Plate { get; set; }
        public string DistrictName { get; set; }
    }
}
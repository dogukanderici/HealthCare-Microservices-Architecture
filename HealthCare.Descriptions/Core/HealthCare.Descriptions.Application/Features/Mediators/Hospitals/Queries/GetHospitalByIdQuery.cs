using HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Queries
{
    public class GetHospitalByIdQuery : IRequest<InternalHandlerResponse<GetHospitalByIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetHospitalByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

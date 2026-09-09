using HealthCare.Descriptions.Application.Features.Mediators.Districts.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Districts.Queries
{
    public class GetDistrictByIdQuery : IRequest<InternalHandlerResponse<GetDistrictByIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetDistrictByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

using HealthCare.Descriptions.Application.Features.Mediators.Cities.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Cities.Queries
{
    public class GetCityByIdQuery : IRequest<InternalHandlerResponse<GetCityByIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetCityByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

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
    public class GetCitiesByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetCitiesByFilterQueryResult>>>
    {
        public int Plate { get; set; }

        public GetCitiesByFilterQuery(int plate)
        {
            Plate = plate;
        }
    }
}

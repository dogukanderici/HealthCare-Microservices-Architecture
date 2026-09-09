using HealthCare.Descriptions.Application.Features.Mediators.Cities.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Cities.Queries
{
    public class GetCitiesByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetCitiesByFilterQueryResult>>>
    {
        public int? Plate { get; set; }


        [JsonConstructor]
        private GetCitiesByFilterQuery()
        {

        }

        public static GetCitiesByFilterQuery Filter(int? plate) =>
            new GetCitiesByFilterQuery
            {
                Plate = plate
            };
    }
}

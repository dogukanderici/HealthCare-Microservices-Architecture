using HealthCare.Descriptions.Application.Features.Mediator.Results.CityResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.CityQueries
{
    public class GetCitiesByFilterQuery : IRequest<List<GetCitiesByFilterQueryResult>>
    {
        public int Plate { get; set; }
        public string CityName { get; set; }

        public GetCitiesByFilterQuery(int plate)
        {
            Plate = plate;
        }

        public GetCitiesByFilterQuery(string cityName)
        {
            CityName = cityName;
        }
    }
}

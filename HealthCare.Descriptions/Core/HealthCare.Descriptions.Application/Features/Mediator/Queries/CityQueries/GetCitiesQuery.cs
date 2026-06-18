using HealthCare.Descriptions.Application.Features.Mediator.Results.CityResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.CityQueries
{
    public class GetCitiesQuery : IRequest<List<GetCitiesQueryResult>>
    {
    }
}

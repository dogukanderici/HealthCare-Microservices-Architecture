using HealthCare.Descriptions.Application.Features.Mediator.Results.ServiceResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.ServiceQueries
{
    public class GetServiceTypesQuery : IRequest<List<GetServiceTypesQueryResult>>
    {
    }
}

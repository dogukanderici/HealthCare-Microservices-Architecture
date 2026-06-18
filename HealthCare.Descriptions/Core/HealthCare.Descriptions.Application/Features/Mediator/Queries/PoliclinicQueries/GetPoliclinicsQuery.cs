using HealthCare.Descriptions.Application.Features.Mediator.Results.PoliclinicResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.PoliclinicQueries
{
    public class GetPoliclinicsQuery : IRequest<List<GetPoliclinicsQueryResult>>
    {
    }
}

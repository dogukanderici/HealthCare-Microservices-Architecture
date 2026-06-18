using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.ServiceQueries
{
    internal class GetServiceTypeCountQuery : IRequest<int>
    {
    }
}

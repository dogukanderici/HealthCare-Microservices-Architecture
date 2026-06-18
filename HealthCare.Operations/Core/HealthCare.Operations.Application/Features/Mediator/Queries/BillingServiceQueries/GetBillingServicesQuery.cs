using HealthCare.Operations.Application.Features.Mediator.Results.BillingServiceResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Queries.BillingServiceQueries
{
    public class GetBillingServicesQuery : IRequest<List<GetBillingServicesQueryResult>>
    {
    }
}

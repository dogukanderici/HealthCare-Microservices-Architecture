using HealthCare.Descriptions.Application.Features.Mediator.Results.QuotaTypeResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.QuotaTypeQueries
{
    public class GetQuotaTypesQuery : IRequest<List<GetQuotaTypesQueryResult>>
    {
    }
}

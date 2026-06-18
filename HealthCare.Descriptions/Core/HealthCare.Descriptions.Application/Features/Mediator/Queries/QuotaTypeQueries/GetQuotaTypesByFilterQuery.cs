using HealthCare.Descriptions.Application.Features.Mediator.Results.QuotaTypeResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.QuotaTypeQueries
{
    public class GetQuotaTypesByFilterQuery : IRequest<List<GetQuotaTypesByFilterQueryResult>>
    {
        public string? QuotaTypeName { get; set; }
        public bool? IsAvailable { get; set; }

        private GetQuotaTypesByFilterQuery() { }

        public static GetQuotaTypesByFilterQuery Filter(string? quotaTypeName, bool? ısAvailable) => new GetQuotaTypesByFilterQuery
        {
            QuotaTypeName = quotaTypeName,
            IsAvailable = ısAvailable
        };
    }
}

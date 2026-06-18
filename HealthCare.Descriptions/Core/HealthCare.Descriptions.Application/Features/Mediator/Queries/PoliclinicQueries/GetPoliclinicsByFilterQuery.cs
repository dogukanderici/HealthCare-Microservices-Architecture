using HealthCare.Descriptions.Application.Features.Mediator.Results.PoliclinicResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.PoliclinicQueries
{
    public class GetPoliclinicsByFilterQuery : IRequest<List<GetPoliclinicsByFilterQueryResult>>
    {
        public int? PoliclinicCode { get; set; }
        public string? PoliclinicName { get; set; }
        public bool? IsAvailable { get; set; }

        private GetPoliclinicsByFilterQuery() { }

        public static GetPoliclinicsByFilterQuery Filter(int? policlinicCode, string? policlinicName, bool? isAvailable) => new GetPoliclinicsByFilterQuery
        {
            PoliclinicCode = policlinicCode,
            PoliclinicName = policlinicName,
            IsAvailable = isAvailable
        };
    }
}

using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Cities.Queries
{
    public class GetCitiesQuery : PagedQueryBase, IRequest<InternalHandlerResponse<IReadOnlyCollection<GetCitiesQueryResult>>>
    {
        public string? PaginationToken { get; set; } = null;
    }
}
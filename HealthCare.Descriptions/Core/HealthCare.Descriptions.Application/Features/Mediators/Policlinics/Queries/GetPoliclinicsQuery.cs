using HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Queries
{
    public class GetPoliclinicsQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetPoliclinicsQueryResult>>>
    {
    }
}

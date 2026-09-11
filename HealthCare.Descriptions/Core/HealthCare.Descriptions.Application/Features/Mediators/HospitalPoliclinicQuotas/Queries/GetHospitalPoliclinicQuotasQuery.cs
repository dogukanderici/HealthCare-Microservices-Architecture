using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Queries
{
    public class GetHospitalPoliclinicQuotasQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetHospitalPoliclinicQuotasQueryResult>>>
    {
    }
}

using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalPoliclinicQuotaResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.HospitalPoliclinicQuotaQueries
{
    public class GetHospitalPoliclinicQuotasQuery : IRequest<List<GetHospitalPoliclinicQuotasQueryResult>>
    {
    }
}

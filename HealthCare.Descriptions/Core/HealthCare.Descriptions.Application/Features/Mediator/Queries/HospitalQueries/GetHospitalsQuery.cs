using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.HospitalQueries
{
    public class GetHospitalsQuery : IRequest<List<GetHospitalsQueryResult>>
    {
    }
}

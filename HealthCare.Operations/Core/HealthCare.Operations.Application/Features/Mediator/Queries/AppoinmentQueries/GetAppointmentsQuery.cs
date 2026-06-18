using HealthCare.Operations.Application.Features.Mediator.Results.AppoinmentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Queries.AppoinmentQueries
{
    public class GetAppointmentsQuery : IRequest<List<GetAppointmentsQueryResult>>
    {
    }
}

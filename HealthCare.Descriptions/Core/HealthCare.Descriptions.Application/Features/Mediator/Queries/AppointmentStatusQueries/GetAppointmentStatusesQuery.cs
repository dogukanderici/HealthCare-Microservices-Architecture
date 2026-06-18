using HealthCare.Descriptions.Application.Features.Mediator.Results.AppointmentStatusResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.AppointmentStatusQueries
{
    public class GetAppointmentStatusesQuery : IRequest<List<GetAppointmentStatusesQueryResult>>
    {
    }
}

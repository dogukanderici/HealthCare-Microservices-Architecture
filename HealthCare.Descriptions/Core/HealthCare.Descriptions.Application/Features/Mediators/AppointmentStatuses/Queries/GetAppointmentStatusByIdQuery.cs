using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Queries
{
    public class GetAppointmentStatusByIdQuery : IRequest<InternalHandlerResponse<GetAppointmentStatusByIdResult>>
    {
        public Guid Id { get; set; }

        public GetAppointmentStatusByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
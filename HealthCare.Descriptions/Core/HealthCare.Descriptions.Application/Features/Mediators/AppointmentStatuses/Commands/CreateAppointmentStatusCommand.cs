using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Commands
{
    public class CreateAppointmentStatusCommand : IRequest<InternalHandlerResponse<Guid>>, ITransactionalRequest
    {
        public Guid Id { get; set; }
        public bool IsAvailable { get; set; }
        public string StatusName { get; set; }
    }
}
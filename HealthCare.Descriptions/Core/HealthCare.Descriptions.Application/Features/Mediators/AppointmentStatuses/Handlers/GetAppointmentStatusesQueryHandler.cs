using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Handlers
{
    public class GetAppointmentStatusesQueryHandler : IRequestHandler<GetAppointmentStatusesQuery, InternalHandlerResponse<IReadOnlyCollection<GetAppointmentStatusesResult>>>
    {
        private readonly IAppointmentStatusService<AppointmentStatus, GetAppointmentStatusesResult> _service;

        public GetAppointmentStatusesQueryHandler(IAppointmentStatusService<AppointmentStatus, GetAppointmentStatusesResult> service)
        {
            _service = service;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetAppointmentStatusesResult>>> Handle(GetAppointmentStatusesQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<AppointmentStatus> dBQueryOptions = new DBQueryOptions<AppointmentStatus>();

            IReadOnlyCollection<GetAppointmentStatusesResult> result = await _service.GetDatasAsync(dBQueryOptions);

            return InternalHandlerResponse<IReadOnlyCollection<GetAppointmentStatusesResult>>.Success(result);
        }
    }
}

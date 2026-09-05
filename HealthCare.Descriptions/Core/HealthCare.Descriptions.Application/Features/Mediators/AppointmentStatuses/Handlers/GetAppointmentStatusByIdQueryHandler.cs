using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Handlers
{
    public class GetAppointmentStatusByIdQueryHandler : IRequestHandler<GetAppointmentStatusByIdQuery, InternalHandlerResponse<GetAppointmentStatusByIdResult>>
    {
        private readonly IAppointmentStatusService<AppointmentStatus, GetAppointmentStatusByIdResult> _service;

        public GetAppointmentStatusByIdQueryHandler(IAppointmentStatusService<AppointmentStatus, GetAppointmentStatusByIdResult> service)
        {
            _service = service;
        }

        public async Task<InternalHandlerResponse<GetAppointmentStatusByIdResult>> Handle(GetAppointmentStatusByIdQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<AppointmentStatus> dBQueryOptions = new DBQueryOptions<AppointmentStatus>();

            Expression<Func<AppointmentStatus, bool>> filter = x => x.Id == request.Id;

            dBQueryOptions.filter = filter;

            InternalServiceResponse<GetAppointmentStatusByIdResult> serviceResponse = await _service.GetDataAsync(dBQueryOptions);

            return InternalHandlerResponse<GetAppointmentStatusByIdResult>.Success(serviceResponse.Data);
        }
    }
}
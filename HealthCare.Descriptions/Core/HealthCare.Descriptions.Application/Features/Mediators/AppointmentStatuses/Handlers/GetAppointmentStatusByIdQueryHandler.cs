using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.AppointmentStatutes;
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
        private readonly IAppointmentStatusQueryService _service;

        public GetAppointmentStatusByIdQueryHandler(IAppointmentStatusQueryService service)
        {
            _service = service;
        }

        public async Task<InternalHandlerResponse<GetAppointmentStatusByIdResult>> Handle(GetAppointmentStatusByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetAppointmentStatusByIdResult> serviceResponse = await _service.GetDataAsync<GetAppointmentStatusByIdResult>(request.Id);

            return InternalHandlerResponse<GetAppointmentStatusByIdResult>.Success(serviceResponse.Data);
        }
    }
}
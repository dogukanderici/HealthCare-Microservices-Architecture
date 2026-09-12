using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.AppointmentStatutes;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Handlers
{
    public class CreateAppointmentStatusCommandHandler : IRequestHandler<CreateAppointmentStatusCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IAppointmentStatusCommandService _service;
        private readonly IMapper _mapper;

        public CreateAppointmentStatusCommandHandler(IAppointmentStatusCommandService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateAppointmentStatusCommand request, CancellationToken cancellationToken)
        {
            AppointmentStatus entity = _mapper.Map<AppointmentStatus>(request);

            InternalServiceResponse<Guid> serviceResponse = await _service.CreateAsync(entity);

            return InternalHandlerResponse<Guid>.Success(serviceResponse.Data);
        }
    }
}
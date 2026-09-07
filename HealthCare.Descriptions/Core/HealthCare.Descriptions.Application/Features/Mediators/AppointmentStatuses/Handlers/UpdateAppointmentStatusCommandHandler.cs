using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.AppointmentStatutes;
using HealthCare.Descriptions.Domain.Abstracts;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Handlers
{
    public class UpdateAppointmentStatusCommandHandler : IRequestHandler<UpdateAppointmentStatusCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly IAppointmentStatusCommandService _commandService;
        private readonly IMapper _mapper;

        public UpdateAppointmentStatusCommandHandler(IAppointmentStatusCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateAppointmentStatusCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<AppointmentStatus> existedData = await _commandService.GetDataForUpdateAsync(request.Id);

            _mapper.Map(request, existedData.Data);

            InternalServiceResponse<DateTimeOffset> serviceResponse = await _commandService.UpdateAsync(existedData.Data);

            return InternalHandlerResponse<DateTimeOffset>.Success(serviceResponse.Data);
        }
    }
}
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
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
    public class RemoveAppointmentStatusCommandHandler : IRequestHandler<RemoveAppointmentStatusCommand, InternalHandlerResponse<bool>>
    {
        private readonly IAppointmentStatusCommandService _commandService;

        public RemoveAppointmentStatusCommandHandler(IAppointmentStatusCommandService commandService)
        {
            _commandService = commandService;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(RemoveAppointmentStatusCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResponse = await _commandService.RemoveAsync(request.Id);

            if (serviceResponse.IsSuccess)
            {
                return InternalHandlerResponse<bool>.Success(serviceResponse.Data);
            }

            return InternalHandlerResponse<bool>.Failure();
        }
    }
}
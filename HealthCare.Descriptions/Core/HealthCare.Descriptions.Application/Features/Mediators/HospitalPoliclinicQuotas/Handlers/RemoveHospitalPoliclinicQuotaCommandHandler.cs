using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinicQuotas;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Handlers
{
    public class RemoveHospitalPoliclinicQuotaCommandHandler : IRequestHandler<RemoveHospitalPoliclinicQuotaCommand, InternalHandlerResponse<bool>>
    {
        private readonly IHospitalPoliclinicQuotaCommandService _commandService;

        public RemoveHospitalPoliclinicQuotaCommandHandler(IHospitalPoliclinicQuotaCommandService commandService)
        {
            _commandService = commandService;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(RemoveHospitalPoliclinicQuotaCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResponse = await _commandService.RemoveAsync(request.Id);

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}
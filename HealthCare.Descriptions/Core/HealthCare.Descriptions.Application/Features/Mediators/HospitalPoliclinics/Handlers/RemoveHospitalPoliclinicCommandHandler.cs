using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Handlers
{
    public class RemoveHospitalPoliclinicCommandHandler : IRequestHandler<RemoveHospitalPoliclinicCommand, InternalHandlerResponse<bool>>
    {
        private readonly IHospitalPoliclinicCommandService _commandService;

        public RemoveHospitalPoliclinicCommandHandler(IHospitalPoliclinicCommandService commandService)
        {
            _commandService = commandService;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(RemoveHospitalPoliclinicCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResponse = await _commandService.RemoveAsync(request.Id);

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}

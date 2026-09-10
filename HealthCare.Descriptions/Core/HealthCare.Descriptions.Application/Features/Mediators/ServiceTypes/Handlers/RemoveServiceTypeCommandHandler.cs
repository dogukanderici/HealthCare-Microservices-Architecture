using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.ServiceType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Handlers
{
    public class RemoveServiceTypeCommandHandler : IRequestHandler<RemoveServiceTypeCommand, InternalHandlerResponse<bool>>
    {
        private readonly IServiceTypeCommandService _commandService;

        public RemoveServiceTypeCommandHandler(IServiceTypeCommandService commandService)
        {
            _commandService = commandService;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(RemoveServiceTypeCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResponse = await _commandService.RemoveAsync(request.Id);

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}

using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.DynamicPolicies;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Handlers
{
    public class DeleteDynamicPolicyCommandHandler : IRequestHandler<DeleteDynamicPolicyCommand, InternalHandlerResponse<bool>>
    {
        private readonly IDynamicPolicyCommandService _commandService;

        public DeleteDynamicPolicyCommandHandler(IDynamicPolicyCommandService commandService)
        {
            _commandService = commandService;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(DeleteDynamicPolicyCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResponse = await _commandService.RemoveAsync(request.Id);

            return serviceResponse.ToHandlerResponse();
        }
    }
}

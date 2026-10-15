using HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
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
        public Task<InternalHandlerResponse<bool>> Handle(DeleteDynamicPolicyCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

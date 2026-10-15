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
    public class CreateDynamicPolicyCommandHandler : IRequestHandler<CreateDynamicPolicyCommand, InternalHandlerResponse<Guid>>
    {
        public Task<InternalHandlerResponse<Guid>> Handle(CreateDynamicPolicyCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

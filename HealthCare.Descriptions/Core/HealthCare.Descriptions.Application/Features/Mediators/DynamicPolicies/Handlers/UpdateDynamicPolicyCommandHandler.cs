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
    public class UpdateDynamicPolicyCommandHandler : IRequestHandler<UpdateDynamicPolicyCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        public Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateDynamicPolicyCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

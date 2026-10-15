using HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Queries
{
    public class GetDynamicPolicyByIdQuery : IRequest<InternalHandlerResponse<GetDynamicPolicyByIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetDynamicPolicyByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
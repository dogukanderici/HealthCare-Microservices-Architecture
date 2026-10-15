using HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Queries
{
    public class GetDynamicRoleByIdQuery : IRequest<InternalHandlerResponse<GetDynamicRoleByIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetDynamicRoleByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
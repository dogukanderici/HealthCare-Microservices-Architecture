using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Commands
{
    public class CreateDynamicRoleCommand : IRequest<InternalHandlerResponse<Guid>>, ITransactionalRequest
    {
        public Guid DynamicPolicyId { get; set; }
        public Guid AllowedRoleId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
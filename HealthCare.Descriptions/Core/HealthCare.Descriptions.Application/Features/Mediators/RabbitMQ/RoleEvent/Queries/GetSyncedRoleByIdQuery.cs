using HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.RoleEvent.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.RoleEvent.Queries
{
    public class GetSyncedRoleByIdQuery : IRequest<InternalHandlerResponse<GetSyncedRoleByIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetSyncedRoleByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
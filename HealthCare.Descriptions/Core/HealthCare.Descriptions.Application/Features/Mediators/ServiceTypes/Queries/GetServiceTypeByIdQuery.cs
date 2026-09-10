using HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Queries
{
    public class GetServiceTypeByIdQuery : IRequest<InternalHandlerResponse<GetServiceTypeByIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetServiceTypeByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

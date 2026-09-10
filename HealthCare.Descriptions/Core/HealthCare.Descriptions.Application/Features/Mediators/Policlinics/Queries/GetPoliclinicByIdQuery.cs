using HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Queries
{
    public class GetPoliclinicByIdQuery : IRequest<InternalHandlerResponse<GetPoliclinicByIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetPoliclinicByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

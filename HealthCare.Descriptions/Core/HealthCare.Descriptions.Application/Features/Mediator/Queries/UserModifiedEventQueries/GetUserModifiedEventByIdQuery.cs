using HealthCare.Descriptions.Application.Features.Mediator.Results.UserModifiedEventResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.UserModifiedEventQueries
{
    public class GetUserModifiedEventByIdQuery : IRequest<GetUserModifiedEventByIdQueryResult>
    {
        public Guid Id { get; set; }

        public GetUserModifiedEventByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

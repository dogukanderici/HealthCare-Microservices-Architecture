using HealthCare.Descriptions.Application.Features.Mediator.Results.DistrictResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.DistrictQueries
{
    public class GetDistrictByIdQuery : IRequest<GetDistrictByIdQueryResult>
    {
        public Guid Id { get; set; }

        public GetDistrictByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

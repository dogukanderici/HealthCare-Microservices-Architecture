using HealthCare.Operations.Application.Features.Mediator.Results.BillingServiceResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Queries.BillingServiceQueries
{
    public class GetBillingServicesByFilterQuery : IRequest<List<GetBillingServicesByFilterQueryResult>>
    {
        public Guid? BillingId { get; set; }
        public Guid? ServiceId { get; set; }
        public int? Piece { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? MinTotalAmount { get; set; }
        public decimal? MaxTotalAmount { get; set; }
    }
}

using HealthCare.Operations.Application.Features.Mediator.Queries.BillQueries;
using HealthCare.Operations.Application.Interfaces;
using HealthCare.Operations.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Handlers.BillHandlers
{
    public class GetBillCountQueryHandler : IRequestHandler<GetBillCountQuery, int>
    {
        private readonly IRepository<Bill> _repository;

        public GetBillCountQueryHandler(IRepository<Bill> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(GetBillCountQuery request, CancellationToken cancellationToken)
        {
            int result = await _repository.GetDataCountAsync();

            return result;
        }
    }
}

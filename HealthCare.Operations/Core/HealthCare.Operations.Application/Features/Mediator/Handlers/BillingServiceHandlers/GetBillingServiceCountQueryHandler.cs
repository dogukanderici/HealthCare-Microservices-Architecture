using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Queries.BillingServiceQueries;
using HealthCare.Operations.Application.Interfaces;
using HealthCare.Operations.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Handlers.BillingServiceHandlers
{
    public class GetBillingServiceCountQueryHandler : IRequestHandler<GetBillingServiceCountQuery, int>
    {
        private readonly IRepository<BillingService> _repository;

        public GetBillingServiceCountQueryHandler(IRepository<BillingService> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(GetBillingServiceCountQuery request, CancellationToken cancellationToken)
        {
            int result = await _repository.GetDataCountAsync();

            return result;
        }
    }
}

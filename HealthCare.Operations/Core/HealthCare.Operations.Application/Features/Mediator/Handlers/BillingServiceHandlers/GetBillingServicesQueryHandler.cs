using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Queries.BillingServiceQueries;
using HealthCare.Operations.Application.Features.Mediator.Results.BillingServiceResults;
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
    public class GetBillingServicesQueryHandler : IRequestHandler<GetBillingServicesQuery, List<GetBillingServicesQueryResult>>
    {
        private readonly IRepository<BillingService> _repository;
        private readonly IMapper _mapper;

        public GetBillingServicesQueryHandler(IRepository<BillingService> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetBillingServicesQueryResult>> Handle(GetBillingServicesQuery request, CancellationToken cancellationToken)
        {
            List<BillingService> result = await _repository.GetDatasAsync();

            return _mapper.Map<List<GetBillingServicesQueryResult>>(result);
        }
    }
}

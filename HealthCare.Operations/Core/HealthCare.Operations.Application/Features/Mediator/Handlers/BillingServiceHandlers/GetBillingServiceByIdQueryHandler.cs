using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Queries.BillingServiceQueries;
using HealthCare.Operations.Application.Features.Mediator.Results.BillingServiceResults;
using HealthCare.Operations.Application.Interfaces;
using HealthCare.Operations.Configurations;
using HealthCare.Operations.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Handlers.BillingServiceHandlers
{
    public class GetBillingServiceByIdQueryHandler : IRequestHandler<GetBillingServiceByIdQuery, GetBillingServiceByIdQueryResult>
    {

        private readonly IRepository<BillingService> _repository;
        private readonly IMapper _mapper;

        public GetBillingServiceByIdQueryHandler(IRepository<BillingService> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetBillingServiceByIdQueryResult> Handle(GetBillingServiceByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<BillingService> dbQueryOptions = new DbQueryOptions<BillingService>();

            Expression<Func<BillingService, bool>> filter = bs => bs.Id == request.Id;
            dbQueryOptions.filter = filter;

            BillingService result = await _repository.GetDataAsync(dbQueryOptions);

            return _mapper.Map<GetBillingServiceByIdQueryResult>(result);
        }
    }
}

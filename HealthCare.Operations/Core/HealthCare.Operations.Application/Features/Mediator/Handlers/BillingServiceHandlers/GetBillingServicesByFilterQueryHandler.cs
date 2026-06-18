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
    public class GetBillingServicesByFilterQueryHandler : IRequestHandler<GetBillingServicesByFilterQuery, List<GetBillingServicesByFilterQueryResult>>
    {
        private readonly IRepository<BillingService> _repository;
        private readonly IMapper _mapper;

        public GetBillingServicesByFilterQueryHandler(IRepository<BillingService> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetBillingServicesByFilterQueryResult>> Handle(GetBillingServicesByFilterQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<BillingService> dbQueryOptions = new DbQueryOptions<BillingService>();

            Expression<Func<BillingService, bool>> filter = bs => (
                (!request.BillingId.HasValue || bs.BillingId == request.BillingId)
                &&
                (!request.ServiceId.HasValue || bs.ServiceId == request.ServiceId)
                &&
                (!request.Piece.HasValue || bs.Piece == request.Piece)
                &&
                (!request.UnitPrice.HasValue || bs.UnitPrice == request.UnitPrice)
                &&
                (!request.MinTotalAmount.HasValue || (bs.TotalAmount >= request.MinTotalAmount && bs.TotalAmount <= request.MaxTotalAmount))
            );
            dbQueryOptions.filter = filter;

            List<BillingService> result = await _repository.GetDatasAsync(dbQueryOptions);

            return _mapper.Map<List<GetBillingServicesByFilterQueryResult>>(result);
        }
    }
}

using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Queries.BillQueries;
using HealthCare.Operations.Application.Features.Mediator.Results.BillResults;
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

namespace HealthCare.Operations.Application.Features.Mediator.Handlers.BillHandlers
{
    public class GetBillByIdQueryHandler : IRequestHandler<GetBillByIdQuery, GetBillByIdQueryResult>
    {
        private readonly IRepository<Bill> _respository;
        private readonly IMapper _mapper;

        public GetBillByIdQueryHandler(IRepository<Bill> respository, IMapper mapper)
        {
            _respository = respository;
            _mapper = mapper;
        }

        public async Task<GetBillByIdQueryResult> Handle(GetBillByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Bill> dbQueryOptions = new DbQueryOptions<Bill>();

            Expression<Func<Bill, bool>> filter = b => b.Id == request.Id;
            dbQueryOptions.filter = filter;

            Bill result = await _respository.GetDataAsync(dbQueryOptions);

            return _mapper.Map<GetBillByIdQueryResult>(result);
        }
    }
}

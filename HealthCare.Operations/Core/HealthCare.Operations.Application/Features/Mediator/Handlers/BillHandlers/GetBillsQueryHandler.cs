using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Queries.BillQueries;
using HealthCare.Operations.Application.Features.Mediator.Results.BillResults;
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
    public class GetBillsQueryHandler : IRequestHandler<GetBillsQuery, List<GetBillsQueryResult>>
    {
        private readonly IRepository<Bill> _repository;
        private readonly IMapper _mapper;

        public GetBillsQueryHandler(IRepository<Bill> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetBillsQueryResult>> Handle(GetBillsQuery request, CancellationToken cancellationToken)
        {
            List<Bill> result = await _repository.GetDatasAsync();

            return _mapper.Map<List<GetBillsQueryResult>>(result);
        }
    }
}

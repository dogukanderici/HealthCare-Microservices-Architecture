using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.QuotaTypeQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.QuotaTypeResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.QuotaTypeHandlers
{
    public class GetQuotaTypesQueryHandler : IRequestHandler<GetQuotaTypesQuery, List<GetQuotaTypesQueryResult>>
    {
        private readonly IRepository<QuotaType> _repository;
        private readonly IMapper _mapper;

        public GetQuotaTypesQueryHandler(IRepository<QuotaType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetQuotaTypesQueryResult>> Handle(GetQuotaTypesQuery request, CancellationToken cancellationToken)
        {
            List<QuotaType> result = await _repository.GetDatasAsync();

            return _mapper.Map<List<GetQuotaTypesQueryResult>>(result);
        }
    }
}

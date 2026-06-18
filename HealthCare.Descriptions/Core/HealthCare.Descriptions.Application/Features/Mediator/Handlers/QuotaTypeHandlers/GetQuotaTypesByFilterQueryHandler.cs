using AutoMapper;
using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.QuotaTypeQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.QuotaTypeResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.QuotaTypeHandlers
{
    public class GetQuotaTypesByFilterQueryHandler : IRequestHandler<GetQuotaTypesByFilterQuery, List<GetQuotaTypesByFilterQueryResult>>
    {
        private readonly IRepository<QuotaType> _repository;
        private readonly IMapper _mapper;

        public GetQuotaTypesByFilterQueryHandler(IRepository<QuotaType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetQuotaTypesByFilterQueryResult>> Handle(GetQuotaTypesByFilterQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<QuotaType> dbQueryOptions = new DbQueryOptions<QuotaType>();

            Expression<Func<QuotaType, bool>> filter = qt => (
                (String.IsNullOrEmpty(request.QuotaTypeName) || qt.QuotaTypeName == request.QuotaTypeName)
                &&
                (!request.IsAvailable.HasValue || qt.IsAvailable == request.IsAvailable)
            );
            dbQueryOptions.filter = filter;

            List<QuotaType> result = await _repository.GetDatasAsync(dbQueryOptions);

            return _mapper.Map<List<GetQuotaTypesByFilterQueryResult>>(result);
        }
    }
}
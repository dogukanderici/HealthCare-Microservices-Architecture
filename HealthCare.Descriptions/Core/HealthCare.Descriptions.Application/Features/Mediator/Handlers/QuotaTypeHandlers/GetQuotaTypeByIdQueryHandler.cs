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
    public class GetQuotaTypeByIdQueryHandler : IRequestHandler<GetQuotaTypeByIdQuery, GetQuotaTypeByIdQueryResult>
    {
        private readonly IRepository<QuotaType> _repository;
        private readonly IMapper _mapper;

        public GetQuotaTypeByIdQueryHandler(IRepository<QuotaType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetQuotaTypeByIdQueryResult> Handle(GetQuotaTypeByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<QuotaType> dbQueryOptions = new DbQueryOptions<QuotaType>();

            Expression<Func<QuotaType, bool>> filter = qt => qt.Id == request.Id;
            dbQueryOptions.filter = filter;

            QuotaType result = await _repository.GetDataAsync(dbQueryOptions);

            return _mapper.Map<GetQuotaTypeByIdQueryResult>(result);
        }
    }
}

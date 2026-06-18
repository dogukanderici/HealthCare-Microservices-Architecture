using AutoMapper;
using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.PoliclinicQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.PoliclinicResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.PoliclinicHandlers
{
    public class GetPoliclinicsByFilterQueryHandler : IRequestHandler<GetPoliclinicsByFilterQuery, List<GetPoliclinicsByFilterQueryResult>>
    {

        private readonly IRepository<Policlinic> _repository;
        private readonly IMapper _mapper;

        public GetPoliclinicsByFilterQueryHandler(IRepository<Policlinic> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetPoliclinicsByFilterQueryResult>> Handle(GetPoliclinicsByFilterQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Policlinic> dbQueryOptions = new DbQueryOptions<Policlinic>();

            Expression<Func<Policlinic, bool>> filter = p => (
                (!request.PoliclinicCode.HasValue || p.PoliclinicCode == request.PoliclinicCode)
                &&
                (String.IsNullOrEmpty(request.PoliclinicName) || p.PoliclinicName == request.PoliclinicName)
                &&
                (!request.IsAvailable.HasValue || p.IsAvailable == request.IsAvailable)
            );

            dbQueryOptions.filter = filter;

            List<Policlinic> result = await _repository.GetDatasAsync(dbQueryOptions);

            return _mapper.Map<List<GetPoliclinicsByFilterQueryResult>>(result);
        }
    }
}

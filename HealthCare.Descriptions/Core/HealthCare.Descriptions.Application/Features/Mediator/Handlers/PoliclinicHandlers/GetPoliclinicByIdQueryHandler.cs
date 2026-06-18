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
    public class GetPoliclinicByIdQueryHandler : IRequestHandler<GetPoliclinicByIdQuery, GetPoliclinicByIdQueryResult>
    {

        private readonly IRepository<Policlinic> _repository;
        private readonly IMapper _mapper;

        public GetPoliclinicByIdQueryHandler(IRepository<Policlinic> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetPoliclinicByIdQueryResult> Handle(GetPoliclinicByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Policlinic> dbQueryOptions = new DbQueryOptions<Policlinic>();

            Expression<Func<Policlinic, bool>> filter = p => p.Id == request.Id;
            dbQueryOptions.filter = filter;

            Policlinic result = await _repository.GetDataAsync(dbQueryOptions);

            return _mapper.Map<GetPoliclinicByIdQueryResult>(result);
        }
    }
}

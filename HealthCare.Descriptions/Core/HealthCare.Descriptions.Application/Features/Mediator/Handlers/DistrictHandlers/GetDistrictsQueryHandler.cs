using AutoMapper;
using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.DistrictQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.DistrictResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.DistrictHandlers
{
    public class GetDistrictsQueryHandler : IRequestHandler<GetDistrictsQuery, List<GetDistrictsQueryResult>>
    {
        private readonly IRepository<District> _repository;
        private readonly IMapper _mapper;

        public GetDistrictsQueryHandler(IRepository<District> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetDistrictsQueryResult>> Handle(GetDistrictsQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<District> dbQueryOptions = new DbQueryOptions<District>();

            List<Expression<Func<District, object>>> includes = new List<Expression<Func<District, object>>>
            {
                d=>d.City
            };
            dbQueryOptions.includes = includes;

            List<District> result = await _repository.GetDatasAsync(dbQueryOptions);

            return _mapper.Map<List<GetDistrictsQueryResult>>(result);
        }
    }
}

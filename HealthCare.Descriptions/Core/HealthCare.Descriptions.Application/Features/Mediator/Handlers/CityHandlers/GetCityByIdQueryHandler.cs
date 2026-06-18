using AutoMapper;
using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.CityQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.CityResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.CityHandlers
{
    public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, GetCityByIdQueryResult>
    {
        private readonly IRepository<City> _repository;
        private readonly IMapper _mapper;

        public GetCityByIdQueryHandler(IRepository<City> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetCityByIdQueryResult> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<City> dbQueryOptions = new DbQueryOptions<City>();

            Expression<Func<City, bool>> filter = c => c.Id == request.Id;
            dbQueryOptions.filter = filter;

            City result = await _repository.GetDataAsync(dbQueryOptions);

            return _mapper.Map<GetCityByIdQueryResult>(result);
        }
    }
}

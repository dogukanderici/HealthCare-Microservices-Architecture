using AutoMapper;
using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.CityQueries;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.CityHandlers
{
    public class GetCityCountQueryHandler : IRequestHandler<GetCityCountQuery, int>
    {
        private readonly IRepository<City> _repository;
        private readonly IMapper _mapper;

        public GetCityCountQueryHandler(IRepository<City> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetCityCountQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<City> dbQueryOptions = new DbQueryOptions<City>();

            int result = await _repository.GetAllDataCountAsync(dbQueryOptions);

            return result;
        }
    }
}

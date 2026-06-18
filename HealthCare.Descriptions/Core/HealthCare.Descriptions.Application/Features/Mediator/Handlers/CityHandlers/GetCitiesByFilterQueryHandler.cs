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
    public class GetCitiesByFilterQueryHandler : IRequestHandler<GetCitiesByFilterQuery, List<GetCitiesByFilterQueryResult>>
    {
        private readonly IRepository<City> _repository;
        private readonly IMapper _mapper;

        public GetCitiesByFilterQueryHandler(IRepository<City> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetCitiesByFilterQueryResult>> Handle(GetCitiesByFilterQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<City> dbQueryOptions = new DbQueryOptions<City>();

            if (request.Plate != null)
            {
                Expression<Func<City, bool>> filter = c => c.Plate == request.Plate;
                dbQueryOptions.filter = filter;
            }

            if (request.CityName != null)
            {
                Expression<Func<City, bool>> filter = c => c.CityName == request.CityName;
                dbQueryOptions.filter = filter;
            }

            List<City> result = await _repository.GetDatasAsync(dbQueryOptions);

            return _mapper.Map<List<GetCitiesByFilterQueryResult>>(result);
        }
    }
}

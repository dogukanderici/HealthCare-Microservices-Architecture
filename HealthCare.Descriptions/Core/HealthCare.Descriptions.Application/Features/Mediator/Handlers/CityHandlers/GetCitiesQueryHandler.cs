using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.CityQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.CityResults;
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
    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, List<GetCitiesQueryResult>>
    {
        private readonly IRepository<City> _repository;
        private readonly IMapper _mapper;

        public GetCitiesQueryHandler(IRepository<City> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetCitiesQueryResult>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            List<City> result = await _repository.GetDatasAsync();

            return _mapper.Map<List<GetCitiesQueryResult>>(result);
        }
    }
}

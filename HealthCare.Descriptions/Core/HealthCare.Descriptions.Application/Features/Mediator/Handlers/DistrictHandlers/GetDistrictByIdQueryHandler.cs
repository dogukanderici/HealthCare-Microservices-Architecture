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
    public class GetDistrictByIdQueryHandler : IRequestHandler<GetDistrictByIdQuery, GetDistrictByIdQueryResult>
    {
        private readonly IRepository<District> _repository;
        private readonly IMapper _mapper;

        public GetDistrictByIdQueryHandler(IRepository<District> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetDistrictByIdQueryResult> Handle(GetDistrictByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<District> dbQueryOptions = new DbQueryOptions<District>();

            Expression<Func<District, bool>> filter = d => d.Id == request.Id;
            dbQueryOptions.filter = filter;

            District result = await _repository.GetDataAsync(dbQueryOptions);

            return _mapper.Map<GetDistrictByIdQueryResult>(result);
        }
    }
}

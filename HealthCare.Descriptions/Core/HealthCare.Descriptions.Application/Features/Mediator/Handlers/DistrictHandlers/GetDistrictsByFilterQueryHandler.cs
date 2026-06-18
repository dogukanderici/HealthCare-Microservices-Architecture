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
    public class GetDistrictsByFilterQueryHandler : IRequestHandler<GetDistrictsByFilterQuery, List<GetDistrictsByFilterQueryResult>>
    {
        private readonly IRepository<District> _repository;
        private readonly IMapper _mapper;

        public GetDistrictsByFilterQueryHandler(IRepository<District> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetDistrictsByFilterQueryResult>> Handle(GetDistrictsByFilterQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<District> dbQueryOptions = new DbQueryOptions<District>();

            Expression<Func<District, bool>> filter = d => (
                (!request.Plate.HasValue || d.Plate == request.Plate)
                &&
                (String.IsNullOrEmpty(request.DistrictName) || d.DistrictName == request.DistrictName)
                &&
                (!request.IsAvailable.HasValue || d.IsAvailable == request.IsAvailable)
            );

            dbQueryOptions.filter = filter;

            List<District> result = await _repository.GetDatasAsync(dbQueryOptions);

            return _mapper.Map<List<GetDistrictsByFilterQueryResult>>(result);
        }
    }
}

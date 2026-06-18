using AutoMapper;
using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.HospitalQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.HospitalHandlers
{
    public class GetHospitalsByFilterQueryHandler : IRequestHandler<GetHospitalsByFilterQuery, List<GetHospitalsByFilterQueryResult>>
    {
        private readonly IRepository<Hospital> _repository;
        private readonly IMapper _mapper;

        public GetHospitalsByFilterQueryHandler(IRepository<Hospital> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetHospitalsByFilterQueryResult>> Handle(GetHospitalsByFilterQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Hospital> dbQueryOptions = new DbQueryOptions<Hospital>();

            Expression<Func<Hospital, bool>> filter = h => (
                (!request.HospitalCode.HasValue || h.HospitalCode == request.HospitalCode)
                &&
                (!request.HospitalCity.HasValue || h.HospitalCity == request.HospitalCity)
                &&
                (!request.HospitalDistrict.HasValue || h.HospitalDistrict == request.HospitalDistrict)
                &&
                (String.IsNullOrEmpty(request.HospitalName) || h.HospitalName == request.HospitalName)
                &&
                (!request.IsAvailable.HasValue || h.IsAvailable == request.IsAvailable)
            );

            Dictionary<Expression<Func<Hospital, object>>, List<Expression<Func<object, object>>>> thenIncludes =
                new Dictionary<Expression<Func<Hospital, object>>, List<Expression<Func<object, object>>>>
                {
                    {
                        h=>h.City,
                        new List<Expression<Func<object, object>>>{}
                    },
                    {
                        h=>h.District,
                        new List<Expression<Func<object, object>>>{}
                    }
                };

            dbQueryOptions.filter = filter;
            dbQueryOptions.thenIncludes = thenIncludes;

            List<Hospital> result = await _repository.GetDatasAsync(dbQueryOptions);

            return _mapper.Map<List<GetHospitalsByFilterQueryResult>>(result);
        }
    }
}

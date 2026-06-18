using AutoMapper;
using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.HospitalPoliclinicQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalPoliclinicResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.HospitalPoliclinicHandlers
{
    public class GetHospitalPoliclinicsQueryHandler : IRequestHandler<GetHospitalPoliclinicsQuery, List<GetHospitalPoliclinicsQueryResult>>
    {
        private readonly IRepository<HospitalPoliclinic> _repository;
        private readonly IMapper _mapper;

        public GetHospitalPoliclinicsQueryHandler(IRepository<HospitalPoliclinic> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetHospitalPoliclinicsQueryResult>> Handle(GetHospitalPoliclinicsQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<HospitalPoliclinic> dbQueryOptions = new DbQueryOptions<HospitalPoliclinic>();

            Dictionary<Expression<Func<HospitalPoliclinic, object>>, List<Expression<Func<object, object>>>> thenIncludes =
                new Dictionary<Expression<Func<HospitalPoliclinic, object>>, List<Expression<Func<object, object>>>>
                {
                    {
                        hp=>hp.Hospital,
                        new List<Expression<Func<object, object>>>{
                            c=>((Hospital)c).City
                        }
                    },
                    {
                        hp=>hp.Policlinic,
                        new List<Expression<Func<object, object>>>{}
                    }
                };
            dbQueryOptions.thenIncludes = thenIncludes;

            List<HospitalPoliclinic> result = await _repository.GetDatasAsync(dbQueryOptions);

            return _mapper.Map<List<GetHospitalPoliclinicsQueryResult>>(result);
        }
    }
}

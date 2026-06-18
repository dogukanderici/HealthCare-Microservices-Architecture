using AutoMapper;
using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.HospitalPoliclinicQuotaQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalPoliclinicQuotaResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.HospitalPoliclinicQuotaHandlers
{
    public class GetHospitalPoliclinicQuotasQueryHandler : IRequestHandler<GetHospitalPoliclinicQuotasQuery, List<GetHospitalPoliclinicQuotasQueryResult>>
    {
        private readonly IRepository<HospitalPoliclinicQuota> _repository;
        private readonly IMapper _mapper;

        public GetHospitalPoliclinicQuotasQueryHandler(IRepository<HospitalPoliclinicQuota> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetHospitalPoliclinicQuotasQueryResult>> Handle(GetHospitalPoliclinicQuotasQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<HospitalPoliclinicQuota> dbQueryOptions = new DbQueryOptions<HospitalPoliclinicQuota>();

            Dictionary<Expression<Func<HospitalPoliclinicQuota, object>>, List<Expression<Func<object, object>>>> thenIncludes =
                new Dictionary<Expression<Func<HospitalPoliclinicQuota, object>>, List<Expression<Func<object, object>>>>
                {
                    {
                        hpq=>hpq.HospitalPoliclinic,
                        new List<Expression<Func<object, object>>>
                        {
                            h=>((HospitalPoliclinic)h).Hospital
                        }
                    },
                    {
                        hpq=>hpq.HospitalPoliclinic,
                        new List<Expression<Func<object, object>>>
                        {
                            p=>((HospitalPoliclinic)p).Policlinic
                        }
                    }
                };
            dbQueryOptions.thenIncludes = thenIncludes;

            List<HospitalPoliclinicQuota> result = await _repository.GetDatasAsync(dbQueryOptions);

            return _mapper.Map<List<GetHospitalPoliclinicQuotasQueryResult>>(result);
        }
    }
}

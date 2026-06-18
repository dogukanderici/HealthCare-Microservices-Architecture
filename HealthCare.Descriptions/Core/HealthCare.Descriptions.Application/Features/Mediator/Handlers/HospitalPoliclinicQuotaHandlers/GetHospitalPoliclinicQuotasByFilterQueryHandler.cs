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
    public class GetHospitalPoliclinicQuotasByFilterQueryHandler : IRequestHandler<GetHospitalPoliclinicQuotasByFilterQuery, List<GetHospitalPoliclinicQuotasByFilterQueryResult>>
    {
        private readonly IRepository<HospitalPoliclinicQuota> _repository;
        private readonly IMapper _mapper;

        public GetHospitalPoliclinicQuotasByFilterQueryHandler(IRepository<HospitalPoliclinicQuota> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetHospitalPoliclinicQuotasByFilterQueryResult>> Handle(GetHospitalPoliclinicQuotasByFilterQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<HospitalPoliclinicQuota> dbQueryOptions = new DbQueryOptions<HospitalPoliclinicQuota>();

            Expression<Func<HospitalPoliclinicQuota, bool>> filter = hpq => (
                (!request.QuotaType.HasValue || hpq.QuotaType == request.QuotaType)
                &&
                (!request.ValidityDate.HasValue || hpq.ValidityDate == request.ValidityDate)
                &&
                (!request.IsAvailable.HasValue || hpq.IsAvailable == request.IsAvailable)
            );
            dbQueryOptions.filter = filter;

            List<HospitalPoliclinicQuota> result = await _repository.GetDatasAsync(dbQueryOptions);

            return _mapper.Map<List<GetHospitalPoliclinicQuotasByFilterQueryResult>>(result);
        }
    }
}

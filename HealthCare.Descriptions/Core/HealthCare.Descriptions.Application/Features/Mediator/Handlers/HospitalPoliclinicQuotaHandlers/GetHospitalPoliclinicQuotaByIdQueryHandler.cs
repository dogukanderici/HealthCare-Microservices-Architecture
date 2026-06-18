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
    public class GetHospitalPoliclinicQuotaByIdQueryHandler : IRequestHandler<GetHospitalPoliclinicQuotaByIdQuery, GetHospitalPoliclinicQuotaByIdQueryResult>
    {

        private readonly IRepository<HospitalPoliclinicQuota> _repository;
        private readonly IMapper _mapper;

        public GetHospitalPoliclinicQuotaByIdQueryHandler(IRepository<HospitalPoliclinicQuota> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetHospitalPoliclinicQuotaByIdQueryResult> Handle(GetHospitalPoliclinicQuotaByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<HospitalPoliclinicQuota> dbQueryOptions = new DbQueryOptions<HospitalPoliclinicQuota>();

            Expression<Func<HospitalPoliclinicQuota, bool>> filter = hpq => hpq.Id == request.Id;
            dbQueryOptions.filter = filter;

            HospitalPoliclinicQuota result = await _repository.GetDataAsync(dbQueryOptions);

            return _mapper.Map<GetHospitalPoliclinicQuotaByIdQueryResult>(result);
        }
    }
}

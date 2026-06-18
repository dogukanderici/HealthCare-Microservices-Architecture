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
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.HospitalPoliclinicHandlers
{
    public class GetHospitalPoliclinicsByFilterQueryHandler :
        IRequestHandler<GetHospitalPoliclinicsByFilterQuery, List<GetHospitalPoliclinicsByFilterQueryResult>>
    {
        private readonly IRepository<HospitalPoliclinic> _repository;
        private readonly IMapper _mapper;

        public GetHospitalPoliclinicsByFilterQueryHandler(IRepository<HospitalPoliclinic> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetHospitalPoliclinicsByFilterQueryResult>> Handle(GetHospitalPoliclinicsByFilterQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<HospitalPoliclinic> dbQueryOptions = new DbQueryOptions<HospitalPoliclinic>();

            Expression<Func<HospitalPoliclinic, bool>> filter = hp => (
                (!request.HospitalId.HasValue || hp.HospitalId == request.HospitalId)
                &&
                (!request.PoliclinicId.HasValue || hp.PoliclinicId == request.PoliclinicId)
                &&
                (!request.IsAvailable.HasValue || hp.IsAvailable == request.IsAvailable)
            );

            dbQueryOptions.filter = filter;

            List<HospitalPoliclinic> result = await _repository.GetDatasAsync(dbQueryOptions);

            return _mapper.Map<List<GetHospitalPoliclinicsByFilterQueryResult>>(result);
        }
    }
}

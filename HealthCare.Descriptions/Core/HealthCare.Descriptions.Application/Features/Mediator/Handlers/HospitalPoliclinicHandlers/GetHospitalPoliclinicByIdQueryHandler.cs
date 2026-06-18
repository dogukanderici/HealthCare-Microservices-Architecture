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
    public class GetHospitalPoliclinicByIdQueryHandler : IRequestHandler<GetHospitalPoliclinicByIdQuery, GetHospitalPoliclinicByIdQueryResult>
    {

        private readonly IRepository<HospitalPoliclinic> _repository;
        private readonly IMapper _mapper;

        public GetHospitalPoliclinicByIdQueryHandler(IRepository<HospitalPoliclinic> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetHospitalPoliclinicByIdQueryResult> Handle(GetHospitalPoliclinicByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<HospitalPoliclinic> dbQueryOptions = new DbQueryOptions<HospitalPoliclinic>();

            Expression<Func<HospitalPoliclinic, bool>> filter = hp => hp.Id == request.Id;
            dbQueryOptions.filter = filter;

            HospitalPoliclinic result = await _repository.GetDataAsync(dbQueryOptions);

            return _mapper.Map<GetHospitalPoliclinicByIdQueryResult>(result);
        }
    }
}

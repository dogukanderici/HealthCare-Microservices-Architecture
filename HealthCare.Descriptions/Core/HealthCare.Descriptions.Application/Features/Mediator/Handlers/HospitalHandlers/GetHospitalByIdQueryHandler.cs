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
    public class GetHospitalByIdQueryHandler : IRequestHandler<GetHospitalByIdQuery, GetHospitalByIdQueryResult>
    {
        private readonly IRepository<Hospital> _repository;
        private readonly IMapper _mapper;

        public GetHospitalByIdQueryHandler(IRepository<Hospital> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetHospitalByIdQueryResult> Handle(GetHospitalByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Hospital> dbQueryOptions = new DbQueryOptions<Hospital>();

            Expression<Func<Hospital, bool>> filter = h => h.Id == request.Id;
            dbQueryOptions.filter = filter;

            Hospital result = await _repository.GetDataAsync(dbQueryOptions);

            return _mapper.Map<GetHospitalByIdQueryResult>(result);
        }
    }
}

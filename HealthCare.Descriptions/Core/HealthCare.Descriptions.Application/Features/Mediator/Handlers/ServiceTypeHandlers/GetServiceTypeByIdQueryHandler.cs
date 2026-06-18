using AutoMapper;
using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.ServiceQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.ServiceResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.ServiceHandlers
{
    public class GetServiceTypeByIdQueryHandler : IRequestHandler<GetServiceTypeByIdQuery, GetServiceTypeByIdQueryResult>
    {
        private readonly IRepository<ServiceType> _repository;
        private readonly IMapper _mapper;

        public GetServiceTypeByIdQueryHandler(IRepository<ServiceType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetServiceTypeByIdQueryResult> Handle(GetServiceTypeByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<ServiceType> dbQueryOptions = new DbQueryOptions<ServiceType>();

            Expression<Func<ServiceType, bool>> filter = st => st.Id == request.Id;
            dbQueryOptions.filter = filter;

            ServiceType result = await _repository.GetDataAsync(dbQueryOptions);

            return _mapper.Map<GetServiceTypeByIdQueryResult>(result);
        }
    }
}

using AutoMapper;
using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.ServiceQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.ServiceResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.ServiceHandlers
{
    public class GetServicesByFilterQueryHandler : IRequestHandler<GetServiceTypesByFilterQuery, List<GetServiceTypesByFilterQueryResult>>
    {
        private readonly IRepository<ServiceType> _repository;
        private readonly IMapper _mapper;

        public GetServicesByFilterQueryHandler(IRepository<ServiceType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetServiceTypesByFilterQueryResult>> Handle(GetServiceTypesByFilterQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<ServiceType> dbQueryOptions = new DbQueryOptions<ServiceType>();

            Expression<Func<ServiceType, bool>> filter = st => (
                (!request.IsAvailable.HasValue || st.IsAvailable == request.IsAvailable)
                &&
                (String.IsNullOrEmpty(request.ServiceCode) || st.ServiceCode == request.ServiceCode)
                &&
                (String.IsNullOrEmpty(request.ServiceName) || st.ServiceName == request.ServiceName)
            );

            dbQueryOptions.filter = filter;

            List<ServiceType> result = await _repository.GetDatasAsync(dbQueryOptions);

            return _mapper.Map<List<GetServiceTypesByFilterQueryResult>>(result);

        }
    }
}

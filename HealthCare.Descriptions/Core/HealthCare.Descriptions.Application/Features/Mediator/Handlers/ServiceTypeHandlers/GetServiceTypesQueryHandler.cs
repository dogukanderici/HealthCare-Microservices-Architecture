using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.ServiceQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.ServiceResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.ServiceHandlers
{
    public class GetServiceTypesQueryHandler : IRequestHandler<GetServiceTypesQuery, List<GetServiceTypesQueryResult>>
    {
        private readonly IRepository<ServiceType> _repository;
        private readonly IMapper _mapper;

        public GetServiceTypesQueryHandler(IRepository<ServiceType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetServiceTypesQueryResult>> Handle(GetServiceTypesQuery request, CancellationToken cancellationToken)
        {
            List<ServiceType> result = await _repository.GetDatasAsync();

            return _mapper.Map<List<GetServiceTypesQueryResult>>(result);
        }
    }
}

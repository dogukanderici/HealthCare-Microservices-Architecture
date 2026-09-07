using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.InstanceQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers
{
    public class GetInstancesQueryHandler : IRequestHandler<GetInstancesQuery, InternalHandlerResponse<IReadOnlyCollection<GetInstancesQueryResult>>>
    {
        private readonly IRepository<Instance> _repository;
        private readonly IMapper _mapper;

        public GetInstancesQueryHandler(IRepository<Instance> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetInstancesQueryResult>>> Handle(GetInstancesQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<Instance> dBQueryOptions = new DBQueryOptions<Instance>();

            IReadOnlyCollection<Instance> result = await _repository.GetAllDataAsync(dBQueryOptions);

            IReadOnlyCollection<GetInstancesQueryResult> mappedData = _mapper.Map<IReadOnlyCollection<GetInstancesQueryResult>>(result);

            return InternalHandlerResponse<IReadOnlyCollection<GetInstancesQueryResult>>.Success(mappedData);
        }
    }
}
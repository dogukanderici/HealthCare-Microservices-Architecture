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
    public class GetInstanceCountQueryHandler : IRequestHandler<GetInstanceCountQuery, InternalHandlerResponse<GetInstancesCountQueryResult>>
    {
        private readonly IRepository<Instance> _repository;
        private readonly IMapper _mapper;

        public GetInstanceCountQueryHandler(IRepository<Instance> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<GetInstancesCountQueryResult>> Handle(GetInstanceCountQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<Instance> options = new DBQueryOptions<Instance>();

            int dataCount = await _repository.GetAllDataCountAsync(options);

            return InternalHandlerResponse<GetInstancesCountQueryResult>.Success(_mapper.Map<GetInstancesCountQueryResult>(dataCount));
        }
    }
}

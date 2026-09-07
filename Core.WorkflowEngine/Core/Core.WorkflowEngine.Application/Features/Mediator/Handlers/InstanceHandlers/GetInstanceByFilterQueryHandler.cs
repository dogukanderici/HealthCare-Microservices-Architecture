using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.InstanceQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers
{
    public class GetInstanceByFilterQueryHandler : IRequestHandler<GetInstanceByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetInstancesByFilterQueryResult>>>
    {
        private readonly IRepository<Instance> _repository;
        private readonly IMapper _mapper;

        public GetInstanceByFilterQueryHandler(IRepository<Instance> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetInstancesByFilterQueryResult>>> Handle(GetInstanceByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<Instance> options = new DBQueryOptions<Instance>();

            Expression<Func<Instance, bool>> filter = x => (
                (!request.Number.HasValue || x.Number == request.Number) &&
                (!request.InitiatorWorkItemId.HasValue || x.InitiatorWorkItemId == request.InitiatorWorkItemId) &&
                (!request.Status.HasValue || x.Status == request.Status)
            );

            options.filter = filter;

            IReadOnlyCollection<Instance> result = await _repository.GetAllDataAsync(options);

            return InternalHandlerResponse<IReadOnlyCollection<GetInstancesByFilterQueryResult>>.Success(_mapper.Map<IReadOnlyCollection<GetInstancesByFilterQueryResult>>(result));
        }
    }
}
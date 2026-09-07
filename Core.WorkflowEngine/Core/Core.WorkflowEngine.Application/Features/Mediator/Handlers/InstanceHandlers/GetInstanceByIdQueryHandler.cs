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
    public class GetInstanceByIdQueryHandler : IRequestHandler<GetInstanceByIdQuery, InternalHandlerResponse<GetInstanceByIdQueryResult>>
    {
        private readonly IRepository<Instance> _repository;
        private readonly IMapper _mapper;

        public GetInstanceByIdQueryHandler(IRepository<Instance> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<GetInstanceByIdQueryResult>> Handle(GetInstanceByIdQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<Instance> options = new DBQueryOptions<Instance>();

            Expression<Func<Instance, bool>> filter = x => x.Id == request.Id;
            options.filter = filter;

            Instance result = await _repository.GetDataAsync(options);

            return InternalHandlerResponse<GetInstanceByIdQueryResult>.Success(_mapper.Map<GetInstanceByIdQueryResult>(result));
        }
    }
}
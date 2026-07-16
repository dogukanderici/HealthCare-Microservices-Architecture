using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.InstanceQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
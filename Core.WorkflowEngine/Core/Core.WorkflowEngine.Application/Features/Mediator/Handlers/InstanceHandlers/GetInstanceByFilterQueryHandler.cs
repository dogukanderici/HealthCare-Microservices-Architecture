using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.InstanceQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
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
    public class GetInstanceByFilterQueryHandler : IRequestHandler<GetInstanceByFilterQuery, InternalHandlerResponse<List<GetInstancesByFilterQueryResult>>>
    {
        private readonly IRepository<Instance> _repository;
        private readonly IMapper _mapper;

        public GetInstanceByFilterQueryHandler(IRepository<Instance> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<List<GetInstancesByFilterQueryResult>>> Handle(GetInstanceByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<Instance> options = new DBQueryOptions<Instance>();

            Expression<Func<Instance, bool>> filter = x => (
                (!request.Number.HasValue || x.Number == request.Number) &&
                (!request.InitiatorWorkItemId.HasValue || x.InitiatorWorkItemId == request.InitiatorWorkItemId) &&
                (!request.Status.HasValue || x.Status == request.Status)
            );

            options.filter = filter;

            List<Instance> result = await _repository.GetAllDataAsync(options);

            return InternalHandlerResponse<List<GetInstancesByFilterQueryResult>>.Success(_mapper.Map<List<GetInstancesByFilterQueryResult>>(result));
        }
    }
}

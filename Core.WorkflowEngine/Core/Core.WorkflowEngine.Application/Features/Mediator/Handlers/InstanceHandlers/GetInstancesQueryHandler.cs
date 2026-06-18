using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.InstanceQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers
{
    public class GetInstancesQueryHandler : IRequestHandler<GetInstancesQuery, List<GetInstancesQueryResult>>
    {
        private readonly IRepository<Instance> _repository;
        private readonly IMapper _mapper;

        public GetInstancesQueryHandler(IRepository<Instance> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetInstancesQueryResult>> Handle(GetInstancesQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<Instance> dBQueryOptions = new DBQueryOptions<Instance>();

            List<Instance> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return _mapper.Map<List<GetInstancesQueryResult>>(result);
        }
    }
}
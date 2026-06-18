using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class GetProcessDefinitionByIdQueryHandler : IRequestHandler<GetProcessDefinitionByIdQuery, GetProcessDefinitionByIdQueryResult>
    {
        private readonly IRepository<ProcessDefinition> _repository;
        private readonly ILogger<GetProcessDefinitionByIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetProcessDefinitionByIdQueryHandler(IRepository<ProcessDefinition> repository, ILogger<GetProcessDefinitionByIdQueryHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<GetProcessDefinitionByIdQueryResult> Handle(GetProcessDefinitionByIdQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();

            Expression<Func<ProcessDefinition, bool>> filter = x => x.Id == request.Id;
            dBQueryOptions.filter = filter;

            ProcessDefinition result = await _repository.GetDataAsync(dBQueryOptions);

            return _mapper.Map<GetProcessDefinitionByIdQueryResult>(result);
        }
    }
}

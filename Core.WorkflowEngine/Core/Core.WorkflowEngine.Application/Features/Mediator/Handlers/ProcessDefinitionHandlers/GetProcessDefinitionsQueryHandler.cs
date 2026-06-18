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
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class GetProcessDefinitionsQueryHandler : IRequestHandler<GetProcessDefinitionsQuery, List<GetProcessDefinitionsQueryResult>>
    {
        private readonly IRepository<ProcessDefinition> _repository;
        private readonly ILogger<GetProcessDefinitionsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetProcessDefinitionsQueryHandler(IRepository<ProcessDefinition> repository, ILogger<GetProcessDefinitionsQueryHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<GetProcessDefinitionsQueryResult>> Handle(GetProcessDefinitionsQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();

            List<ProcessDefinition> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return _mapper.Map<List<GetProcessDefinitionsQueryResult>>(result);
        }
    }
}
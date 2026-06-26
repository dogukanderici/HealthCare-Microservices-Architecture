using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
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
    public class GetProcessDefinitionCountQueryHandler : IRequestHandler<GetProcessDefinitionCountQuery, InternalHandlerResponse<GetProcessDefinitionCountQueryResult>>
    {
        private readonly IRepository<ProcessDefinition> _repository;
        private readonly ILogger<GetProcessDefinitionCountQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetProcessDefinitionCountQueryHandler(IRepository<ProcessDefinition> repository, ILogger<GetProcessDefinitionCountQueryHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<GetProcessDefinitionCountQueryResult>> Handle(GetProcessDefinitionCountQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();

            int result = await _repository.GetAllDataCountAsync(dBQueryOptions);

            return InternalHandlerResponse<GetProcessDefinitionCountQueryResult>.Success(_mapper.Map<GetProcessDefinitionCountQueryResult>(result));
        }
    }
}
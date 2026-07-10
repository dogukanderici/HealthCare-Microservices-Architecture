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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class GetProcessDefinitionsByFilterQueryHandler : IRequestHandler<GetProcessDefinitionsByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsByFilterQueryResult>>>
    {
        private readonly IRepository<ProcessDefinition> _repository;
        private readonly ILogger<GetProcessDefinitionsByFilterQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetProcessDefinitionsByFilterQueryHandler(IRepository<ProcessDefinition> repository, ILogger<GetProcessDefinitionsByFilterQueryHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsByFilterQueryResult>>> Handle(GetProcessDefinitionsByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();

            Expression<Func<ProcessDefinition, bool>> filter = x => (
            (!request.IsActive.HasValue || x.IsActive == request.IsActive) &&
            (string.IsNullOrEmpty(request.ProcessName) || x.ProcessName == request.ProcessName)
            );

            dBQueryOptions.filter = filter;

            IReadOnlyCollection<ProcessDefinition> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsByFilterQueryResult>>.Success(_mapper.Map<IReadOnlyCollection<GetProcessDefinitionsByFilterQueryResult>>(result));
        }
    }
}

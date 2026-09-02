using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Configuration.Wrappers;
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
    public class GetProcessDefinitionByIdQueryHandler : IRequestHandler<GetProcessDefinitionByIdQuery, InternalHandlerResponse<GetProcessDefinitionByIdQueryResult>>
    {
        private readonly IProcessDefinitionService _processDefinitionService;
        private readonly IMapper _mapper;

        public GetProcessDefinitionByIdQueryHandler(IProcessDefinitionService processDefinitionService, IMapper mapper)
        {
            _processDefinitionService = processDefinitionService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<GetProcessDefinitionByIdQueryResult>> Handle(GetProcessDefinitionByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<ProcessDefinition> serviceResponse = await _processDefinitionService.GetDataByIdAsync(request.Id);

            return InternalHandlerResponse<GetProcessDefinitionByIdQueryResult>.Success(_mapper.Map<GetProcessDefinitionByIdQueryResult>(serviceResponse.Data));
        }
    }
}
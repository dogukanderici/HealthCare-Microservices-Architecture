using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskActionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskActionResults;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Configuration.Constants;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskActionHandlers
{
    public class GetProcessTaskActionsQueryHandler : IRequestHandler<GetProcessTaskActionsQuery, List<GetProcessTaskActionsQueryResult>>
    {
        private readonly IRepository<ProcessTaskAction> _repository;
        private readonly ILogger<GetProcessTaskActionsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetProcessTaskActionsQueryHandler(IRepository<ProcessTaskAction> repository, ILogger<GetProcessTaskActionsQueryHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<GetProcessTaskActionsQueryResult>> Handle(GetProcessTaskActionsQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTaskAction> dBQueryOptions = new DBQueryOptions<ProcessTaskAction>();

            List<ProcessTaskAction> result = await _repository.GetAllDataAsync(dBQueryOptions);

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                nameof(GetProcessTaskActionsQueryHandler),
                LogConstants.SuccessMessages.ProcessSuccessed);

            return _mapper.Map<List<GetProcessTaskActionsQueryResult>>(result);
        }
    }
}

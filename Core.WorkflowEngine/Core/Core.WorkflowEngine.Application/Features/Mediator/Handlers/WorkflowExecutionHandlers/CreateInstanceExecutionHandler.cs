using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkflowExecutionCommands;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkflowExecutionHandlers
{
    public class CreateInstanceExecutionHandler : IRequestHandler<CreateInstanceExecutionCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IInstanceService _instanceService;
        private readonly IMapper _mapper;

        public CreateInstanceExecutionHandler(IInstanceService instanceService, IMapper mapper)
        {
            _instanceService = instanceService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateInstanceExecutionCommand request, CancellationToken cancellationToken)
        {
            Instance dataFromDto = _mapper.Map<Instance>(request);

            InternalServiceResponse<Guid> result = await _instanceService.CreateAsync(dataFromDto, cancellationToken);

            return InternalHandlerResponse<Guid>.Success(result.Data);
        }
    }
}
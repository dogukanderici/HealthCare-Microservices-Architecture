using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskTransitionCommands;
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

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskTransitionHandlers
{
    public class CreateProcessTaskTransitionCommandHandler : IRequestHandler<CreateProcessTaskTransitionCommand, InternalHandlerResponse<Guid>>
    {
        private readonly ITaskTransitionService _service;
        private readonly IMapper _mapper;

        public CreateProcessTaskTransitionCommandHandler(ITaskTransitionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateProcessTaskTransitionCommand request, CancellationToken cancellationToken)
        {
            ProcessTaskTransition dataFromDto = _mapper.Map<ProcessTaskTransition>(request);

            InternalServiceResponse<Guid> result = await _service.CreateAsync(dataFromDto, cancellationToken);

            if (result.IsSuccess)
            {
                return InternalHandlerResponse<Guid>.Success(result.Data, InternalCommandConstants.SuccessProcessTaskTransitionCreating);
            }

            return InternalHandlerResponse<Guid>.Failure(InternalCommandConstants.ErrorProcessTaskTransitionCreating);
        }
    }
}
using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Features.BusinessRules.WorkItemExecutionPolicies;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkflowExecutionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using Core.WorkflowEngine.Domain.Abstractions;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkflowExecutionHandlers
{
    public class CreateInstanceExecutionHandler : IRequestHandler<CreateInstanceExecutionCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IInstanceCommandService _instanceCommandService;
        private readonly IMapper _mapper;
        private readonly IWorkItemExecutionCreatePolicy _createPolicy;

        public CreateInstanceExecutionHandler(IInstanceCommandService instanceCommandService, IMapper mapper, IWorkItemExecutionCreatePolicy createPolicy)
        {
            _instanceCommandService = instanceCommandService;
            _mapper = mapper;
            _createPolicy = createPolicy;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateInstanceExecutionCommand request, CancellationToken cancellationToken)
        {
            Instance dataFromDto = _mapper.Map<Instance>(request);

            InternalPolicyResponse policyResponse = await _createPolicy.ExecuteAllRuleAsync(dataFromDto);

            if (!policyResponse.IsSuccess)
                return InternalHandlerResponse<Guid>.Failure(policyResponse.PolicyMessage);

            InternalServiceResponse<Guid> result = await _instanceCommandService.CreateAsync(dataFromDto, cancellationToken);

            return InternalHandlerResponse<Guid>.Success(result.Data);
        }
    }
}

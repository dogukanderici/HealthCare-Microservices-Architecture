using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.DynamicPolicyCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.DynamicPolicyServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.DynamicPolicyHandlers
{
    public class UpdateDynamicPolicyCommandHandler : IRequestHandler<UpdateDynamicPolicyCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly IDynamicPolicyCommandService _commandService;
        private readonly IMapper _mapper;

        public UpdateDynamicPolicyCommandHandler(IDynamicPolicyCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }
        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateDynamicPolicyCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<DynamicPolicy> existedData = await _commandService.GetDataForUpdateAsync(request.Id);

            if (!existedData.IsSuccess)
                return InternalHandlerResponse<DateTimeOffset>.Failure(existedData.ServiceMessage);

            _mapper.Map(request, existedData.Data);

            InternalServiceResponse<DateTimeOffset> serviceResponse = await _commandService.UpdateAsync(existedData.Data);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.DynamicRoleCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.DynamicRoleServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.DynamicRoleHandlers
{
    public class UpdateDynamicRoleCommandHandler : IRequestHandler<UpdateDynamicRoleCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly IDynamicRoleCommandService _commandService;
        private readonly IMapper _mapper;

        public UpdateDynamicRoleCommandHandler(IDynamicRoleCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateDynamicRoleCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<DynamicRole> existedData = await _commandService.GetDataForUpdateAsync(request.Id);

            if (!existedData.IsSuccess)
                return InternalHandlerResponse<DateTimeOffset>.Failure(existedData.ServiceMessage);

            _mapper.Map(request, existedData.Data);

            InternalServiceResponse<DateTimeOffset> serviceResponse = await _commandService.UpdateAsync(existedData.Data);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
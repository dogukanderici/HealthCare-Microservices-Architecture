using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.InstanceCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers
{
    public class UpdateInstanceCommandHandler : IRequestHandler<UpdateInstanceCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly IInstanceCommandService _instanceCommandService;
        private readonly IMapper _mapper;

        public UpdateInstanceCommandHandler(IInstanceCommandService instanceCommandService, IMapper mapper)
        {
            _instanceCommandService = instanceCommandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateInstanceCommand request, CancellationToken cancellationToken)
        {

            InternalServiceResponse<Instance> existedData = await _instanceCommandService.GetDataForUpdateAsync(request.Id);

            if (!existedData.IsSuccess)
                return InternalHandlerResponse<DateTimeOffset>.Failure(existedData.ServiceMessage);

            _mapper.Map(request, existedData);

            InternalServiceResponse<DateTimeOffset> serviceResponse = await _instanceCommandService.UpdateAsync(existedData.Data);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
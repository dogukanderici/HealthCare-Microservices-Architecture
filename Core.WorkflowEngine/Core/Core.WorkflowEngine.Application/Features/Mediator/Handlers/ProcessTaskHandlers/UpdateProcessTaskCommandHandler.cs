using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskBusinessRules;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskHandlers
{
    public class UpdateProcessTaskCommandHandler : IRequestHandler<UpdateProcessTaskCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly IProcessTaskCommandService _commandService;
        private readonly IMapper _mapper;

        public UpdateProcessTaskCommandHandler(IProcessTaskCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateProcessTaskCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<ProcessTask> existedData = await _commandService.GetDataForUpdateAsync(request.Id);

            if (!existedData.IsSuccess)
                return InternalHandlerResponse<DateTimeOffset>.Failure(existedData.ServiceMessage);

            _mapper.Map(request, existedData.Data);

            InternalServiceResponse<DateTimeOffset> serviceResponse = await _commandService.UpdateAsync(existedData.Data);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
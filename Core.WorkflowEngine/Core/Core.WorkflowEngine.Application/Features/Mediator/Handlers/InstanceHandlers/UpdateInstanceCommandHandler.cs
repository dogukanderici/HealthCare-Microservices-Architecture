using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.InstanceCommands;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Configuration.Constants;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers
{
    public class UpdateInstanceCommandHandler : IRequestHandler<UpdateInstanceCommand, InternalCommandResponse<DateTimeOffset>>
    {
        private readonly IRepository<Instance> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateInstanceCommandHandler> _logger;

        public UpdateInstanceCommandHandler(IRepository<Instance> repository, IMapper mapper, ILogger<UpdateInstanceCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<InternalCommandResponse<DateTimeOffset>> Handle(UpdateInstanceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Instance dataFromDto = _mapper.Map<Instance>(request);

                DBQueryOptions<Instance> dbQueryOptions = new DBQueryOptions<Instance>();

                Expression<Func<Instance, bool>> filter = x => x.Id == request.Id;
                dbQueryOptions.filter = filter;

                int existingDataCount = await _repository.GetAllDataCountAsync(dbQueryOptions);

                if (existingDataCount < 1)
                {
                    _logger.LogError(LogConstants.LogMessageTemplate,
                        nameof(UpdateInstanceCommandHandler),
                        LogConstants.ErrorMessages.DataNotFound);

                    return InternalCommandResponse<DateTimeOffset>.Failure(InternalCommandConstants.NotFoundData);
                }

                await _repository.UpdateDataAsync(dataFromDto);

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(UpdateInstanceCommandHandler),
                        LogConstants.SuccessMessages.DataUpdatedSuccessfully);

                return InternalCommandResponse<DateTimeOffset>.Success(DateTimeOffset.Now, InternalCommandConstants.SuccessInstanceUpdating);
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                        nameof(UpdateInstanceCommandHandler),
                        ex);

                return InternalCommandResponse<DateTimeOffset>.Failure(InternalCommandConstants.ErrorInstanceUpdating); ;
            }
        }
    }
}
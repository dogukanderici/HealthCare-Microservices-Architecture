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
    public class DeleteInstanceCommandHandler : IRequestHandler<DeleteInstanceCommand, InternalCommandResponse<bool>>
    {
        private readonly IRepository<Instance> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateInstanceCommandHandler> _logger;

        public DeleteInstanceCommandHandler(IRepository<Instance> repository, IMapper mapper, ILogger<UpdateInstanceCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<InternalCommandResponse<bool>> Handle(DeleteInstanceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                DBQueryOptions<Instance> dbQueryOptions = new DBQueryOptions<Instance>();

                Expression<Func<Instance, bool>> filter = x => x.Id == request.Id;
                dbQueryOptions.filter = filter;

                Instance existingData = await _repository.GetDataAsync(dbQueryOptions);

                if (existingData != null)
                {
                    _logger.LogError(LogConstants.LogMessageTemplate,
                        nameof(UpdateInstanceCommandHandler),
                        LogConstants.ErrorMessages.DataNotFound);

                    return InternalCommandResponse<bool>.Failure(InternalCommandConstants.NotFoundData);
                }

                await _repository.DeleteDataAsync(existingData);

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(UpdateInstanceCommandHandler),
                        LogConstants.SuccessMessages.DataDeletedSuccessfully);

                return InternalCommandResponse<bool>.Success(true, InternalCommandConstants.SuccessInstanceDeleting);
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                        nameof(UpdateInstanceCommandHandler),
                        ex);

                return InternalCommandResponse<bool>.Failure(InternalCommandConstants.ErrorInstanceDeleting);
            }
        }
    }
}
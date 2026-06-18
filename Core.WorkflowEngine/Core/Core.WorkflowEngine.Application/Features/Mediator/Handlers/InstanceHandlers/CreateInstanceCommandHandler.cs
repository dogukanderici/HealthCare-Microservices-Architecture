using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.InstanceCommands;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration.Constants;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers
{
    public class CreateInstanceCommandHandler : IRequestHandler<CreateInstanceCommand, InternalCommandResponse<Guid>>
    {
        private readonly IRepository<Instance> _repository;
        private readonly IRepository<WorkItem> _wiRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateInstanceCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CreateInstanceCommandHandler(IRepository<Instance> repository, IRepository<WorkItem> wiRepository, IMapper mapper, ILogger<CreateInstanceCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _wiRepository = wiRepository;
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<InternalCommandResponse<Guid>> Handle(CreateInstanceCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {

                Instance instanceEntity = _mapper.Map<Instance>(request);
                instanceEntity.InitiatorWorkItemId = null;

                Guid instanceId = await _repository.CreateDataAsync(instanceEntity);

                // Circular Dependency hatası fırlatmaması için öncelikle Instances için ön kayıt yapılır.
                await _unitOfWork.CommitAsync(cancellationToken);

                WorkItem workitemEntity = new WorkItem();
                workitemEntity.InstanceId = instanceEntity.Id;

                Guid workitemId = await _wiRepository.CreateDataAsync(workitemEntity);

                instanceEntity.InitiatorWorkItemId = workitemId;

                // WorkItems için ön kayıt yapılır.
                await _unitOfWork.CommitAsync(cancellationToken);

                // Transaction tamamlanır.
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(CreateInstanceCommandHandler),
                        LogConstants.SuccessMessages.DataCreatedSuccessfully);

                return InternalCommandResponse<Guid>.Success(instanceId, InternalCommandConstants.SuccessInstanceCreating);
            }
            catch (Exception ex)
            {

                await _unitOfWork.RollbackTransactionAsync();

                _logger.LogError(LogConstants.LogMessageTemplate,
                        nameof(CreateInstanceCommandHandler),
                        ex);

                return InternalCommandResponse<Guid>.Failure(InternalCommandConstants.ErrorInstanceCreating);
            }
        }
    }
}
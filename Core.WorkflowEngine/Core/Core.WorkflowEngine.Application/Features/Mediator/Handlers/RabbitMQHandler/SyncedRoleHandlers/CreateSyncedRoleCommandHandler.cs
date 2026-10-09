using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.RabbitMQCommands.SyncedRoleCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.RabbitMQHandler.SyncedRoleHandlers
{
    public class CreateSyncedRoleCommandHandler : IRequestHandler<CreateSyncedRoleCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IRepository<SyncedRole> _repository;
        private readonly IMapper _mapper;

        public CreateSyncedRoleCommandHandler(IRepository<SyncedRole> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateSyncedRoleCommand request, CancellationToken cancellationToken)
        {
            SyncedRole dataFromDto = _mapper.Map<SyncedRole>(request);

            Guid repoResponse = await _repository.CreateDataAsync(dataFromDto);

            return InternalHandlerResponse<Guid>.Success(repoResponse);
        }
    }
}
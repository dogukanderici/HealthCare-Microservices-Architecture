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
    public class UpdateSyncedRoleCommandHandler : IRequestHandler<UpdateSyncedRoleCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly IRepository<SyncedRole> _repository;
        private readonly IMapper _mapper;

        public UpdateSyncedRoleCommandHandler(IRepository<SyncedRole> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateSyncedRoleCommand request, CancellationToken cancellationToken)
        {
            SyncedRole dataFromDto = _mapper.Map<SyncedRole>(request);

            DateTimeOffset repoResponse = await _repository.UpdateDataAsync(dataFromDto);

            return InternalHandlerResponse<DateTimeOffset>.Success(repoResponse);
        }
    }
}
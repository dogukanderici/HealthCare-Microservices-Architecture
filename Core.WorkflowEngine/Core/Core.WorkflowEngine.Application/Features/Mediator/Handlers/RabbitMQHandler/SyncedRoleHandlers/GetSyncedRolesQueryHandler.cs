using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.SyncedRoleQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.SyncedRoleResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
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
    public class GetSyncedRolesQueryHandler : IRequestHandler<GetSyncedRolesQuery, InternalHandlerResponse<IReadOnlyCollection<GetSyncedRolesQueryResult>>>
    {
        private readonly IRepository<SyncedRole> _repository;
        private readonly IMapper _mapper;

        public GetSyncedRolesQueryHandler(IRepository<SyncedRole> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetSyncedRolesQueryResult>>> Handle(GetSyncedRolesQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<SyncedRole> repoResponse = await _repository.GetAllDataAsync();

            return InternalHandlerResponse<IReadOnlyCollection<GetSyncedRolesQueryResult>>
                .Success(_mapper.Map<IReadOnlyCollection<GetSyncedRolesQueryResult>>(repoResponse));
        }
    }
}
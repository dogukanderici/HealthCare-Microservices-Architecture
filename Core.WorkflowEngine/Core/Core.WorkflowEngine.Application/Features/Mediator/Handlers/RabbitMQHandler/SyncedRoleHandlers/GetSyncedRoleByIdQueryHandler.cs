using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.SyncedRoleQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.SyncedRoleResults;
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
    public class GetSyncedRoleByIdQueryHandler : IRequestHandler<GetSyncedRoleByIdQuery, InternalHandlerResponse<GetSyncedRoleByIdQueryResult>>
    {
        private readonly IRepository<SyncedRole> _repository;
        private readonly IMapper _mapper;

        public GetSyncedRoleByIdQueryHandler(IRepository<SyncedRole> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<GetSyncedRoleByIdQueryResult>> Handle(GetSyncedRoleByIdQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<SyncedRole> dBQueryOptions = new DBQueryOptions<SyncedRole>();
            dBQueryOptions.filter = x => x.Id == request.Id;

            SyncedRole repoResponse = await _repository.GetDataAsync(dBQueryOptions);

            return InternalHandlerResponse<GetSyncedRoleByIdQueryResult>
                .Success(_mapper.Map<GetSyncedRoleByIdQueryResult>(repoResponse));
        }
    }
}
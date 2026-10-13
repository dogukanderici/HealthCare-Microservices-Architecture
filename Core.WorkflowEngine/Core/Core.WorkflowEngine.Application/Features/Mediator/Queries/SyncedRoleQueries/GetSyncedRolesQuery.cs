using Core.WorkflowEngine.Application.Features.Mediator.Results.SyncedRoleResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.SyncedRoleQueries
{
    public class GetSyncedRolesQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetSyncedRolesQueryResult>>>
    {
    }
}
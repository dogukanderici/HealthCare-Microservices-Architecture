using Core.WorkflowEngine.Application.Features.Mediator.Results.DynamicPolicyResults.Shared;
using Core.WorkflowEngine.Application.Features.Mediator.Results.SyncedRoleResults.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Results.DynamicRoleResults
{
    public class GetDynamicRoleByIdQueryResult
    {
        public Guid Id { get; set; }
        public Guid DynamicPolicyId { get; set; }
        public Guid AllowedRoleId { get; set; }
        public bool IsActive { get; set; }

        // Relations
        public SharedDynamicPolicyResult DynamicPolicy { get; set; }
        public SharedSyncedRoleResult SyncedRole { get; set; }
    }
}
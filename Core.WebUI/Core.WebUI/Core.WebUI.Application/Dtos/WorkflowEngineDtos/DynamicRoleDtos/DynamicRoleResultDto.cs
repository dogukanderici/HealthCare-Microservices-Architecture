using Core.WebUI.Application.Dtos.IdentityDtos.RoleDtos.Shared;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicPolicyDtos.Shared;
using Core.WebUI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicRoleDtos
{
    public class DynamicRoleResultDto : IDto
    {
        public Guid Id { get; set; }
        public Guid DynamicPolicyId { get; set; }
        public Guid AllowedRoleId { get; set; }
        public bool IsActive { get; set; }

        public SharedDynamicPolicyResult DynamicPolicy { get; set; }
        public SharedSyncedRoleResult SyncedRole { get; set; }
    }
}
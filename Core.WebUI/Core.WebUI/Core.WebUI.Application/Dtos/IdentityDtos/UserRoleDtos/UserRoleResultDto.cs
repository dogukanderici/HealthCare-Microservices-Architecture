using Core.WebUI.Application.Dtos.IdentityDtos.RoleDtos.Shared;
using Core.WebUI.Application.Dtos.IdentityDtos.UserDtos.Shared;
using Core.WebUI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Dtos.IdentityDtos.UserRoleDtos
{
    public class UserRoleResultDto : IDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public SharedUserResultDto User { get; set; }
        public SharedSyncedRoleResult Role { get; set; }
    }
}
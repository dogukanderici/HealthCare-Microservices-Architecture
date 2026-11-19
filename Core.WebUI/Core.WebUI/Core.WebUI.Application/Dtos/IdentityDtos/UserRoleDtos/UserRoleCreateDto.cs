using Core.WebUI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Dtos.IdentityDtos.UserRoleDtos
{
    public class UserRoleCreateDto : IDto
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
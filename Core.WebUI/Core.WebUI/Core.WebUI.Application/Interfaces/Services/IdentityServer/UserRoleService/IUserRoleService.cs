using Core.WebUI.Application.Dtos.IdentityDtos.UserRoleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Interfaces.Services.IdentityServer.UserRoleService
{
    public interface IUserRoleService : IBaseService<UserRoleResultDto, UserRoleCreateDto, UserRoleUpdateDto>
    {
    }
}

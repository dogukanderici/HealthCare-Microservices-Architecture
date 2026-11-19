using Core.WebUI.Application.Dtos.IdentityDtos.RoleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Interfaces.Services.IdentityServer.RoleService
{
    public interface IRoleService : IBaseService<RoleResultDto, RoleCreateDto, RoleUpdateDto>
    {
    }
}

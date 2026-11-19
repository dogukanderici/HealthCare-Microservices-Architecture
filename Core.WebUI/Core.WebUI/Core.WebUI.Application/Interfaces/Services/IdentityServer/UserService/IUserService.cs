using Core.WebUI.Application.Dtos.IdentityDtos;
using Core.WebUI.Application.Dtos.IdentityDtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Interfaces.Services.IdentityServer.UserService
{
    public interface IUserService : IBaseService<UserResultDto, UserCreateDto, UserUpdateDto>
    {
    }
}
using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Interfaces.Services.AuthService
{
    public interface IAuthService
    {
        Task<UIApiResponse<bool>> LoginAsync(LoginDto loginDto);
        Task<UIApiResponse<bool>> RegisterAsync(RegisterDto registerDto);
        Task<UIApiResponse<bool>> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    }
}

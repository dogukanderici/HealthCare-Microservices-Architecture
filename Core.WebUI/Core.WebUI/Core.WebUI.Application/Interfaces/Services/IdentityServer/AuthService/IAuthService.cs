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
        Task<bool> LoginAsync(LoginDto loginDto);
        Task<bool> LogoutAsync(LogoutDto logoutDto);
        Task<bool> RegisterAsync(RegisterDto registerDto);
        Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    }
}

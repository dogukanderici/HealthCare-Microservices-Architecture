using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.IdentityDtos;
using Core.WebUI.Application.Interfaces.Services.AuthService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Persistence.ExternalServices.IdentityServer
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Task<UIApiResponse<bool>> LoginAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<bool>> RegisterAsync(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<bool>> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            throw new NotImplementedException();
        }
    }
}
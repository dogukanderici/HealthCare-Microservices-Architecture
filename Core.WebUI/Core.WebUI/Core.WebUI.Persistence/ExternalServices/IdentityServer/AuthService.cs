using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.IdentityDtos;
using Core.WebUI.Application.Interfaces.Services.AuthService;
using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using TokenResponse = Duende.IdentityModel.Client.TokenResponse;

namespace Core.WebUI.Persistence.ExternalServices.IdentityServer
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _identityServerBaseUrl;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public AuthService(IOptions<ApiSetting> option, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _identityServerBaseUrl = option.Value.IdentityServerBaseUrl;
            _clientId = option.Value.ClientId;
            _clientSecret = option.Value.ClientSecret;

            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> LoginAsync(LoginDto loginDto)
        {
            // IdentityServer'dan Token Talep Edilir.
            // Kullanıcı Adı - Şifre bilgisi kullanıldığı için Resource Owner Password tipi kullanılır.

            HttpClient client = _httpClientFactory.CreateClient();

            // GetDiscoveryDocumentAsync kullanılacak request nesnesi oluşturulur.
            DiscoveryDocumentRequest discoveryDocumentRequest = new DiscoveryDocumentRequest
            {
                Address = _identityServerBaseUrl,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            };

            // Duende.IdentityModel.Client kütüphanesi ile IdentityServer'dan discovery document alınır.
            DiscoveryDocumentResponse discoveryDocument = await client.GetDiscoveryDocumentAsync(discoveryDocumentRequest);

            // Token talebi için gerekli olan request nesnesi oluşturulur.
            PasswordTokenRequest passwordTokenRequest = new PasswordTokenRequest
            {
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                UserName = loginDto.Username,
                Password = loginDto.Password,
                Address = discoveryDocument.TokenEndpoint
            };

            // IdentityServer'dan token bilgisi alınır.
            TokenResponse token = await client.RequestPasswordTokenAsync(passwordTokenRequest);

            if (token.HttpStatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            // JWT içerisindeki claim bilgileri okunur.
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token.AccessToken);

            var claimsIdentity = new ClaimsIdentity(jwt.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Token Cookie içerisinde saklanır.
            AuthenticationProperties authProperties = new AuthenticationProperties();

            authProperties.StoreTokens(
                    new List<AuthenticationToken>
                    {
                        new AuthenticationToken{Name="access_token",Value=token.AccessToken },
                        new AuthenticationToken{Name="refresh_token",Value=token.RefreshToken },
                        new AuthenticationToken{Name="expires_at",Value = DateTime.UtcNow.AddSeconds(token.ExpiresIn).ToString("o") }
                    }
                );
            authProperties.IsPersistent = true;

            // Kullanıcı MVC sistemine giriş yapar.
            await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimPrincipal,
                    authProperties
                );

            return true;
        }

        public Task<bool> LogoutAsync(LogoutDto logoutDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            throw new NotImplementedException();
        }
    }
}
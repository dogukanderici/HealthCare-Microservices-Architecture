using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Core.IdentityServer
{
    public class Config
    {
        // API'ye erişim sağlacak kaynak ve kaynağın kapsamları
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("ApplicationPermission")
            {
                Scopes =
                {
                    "ReadPermission",
                    "TokenPermission",
                }
            }
        };

        // Kaynak tanımları
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("ReadPermission","Just Read Permission For Not Member Users",new[]{"scope"}),
            new ApiScope("TokenPermission","Application Permission For Users",new[]{"scope"}),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };

        // Token alınan kullanıcının hangi bilgilerine erişeleceğinin tanımı
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<Client> Clients => new Client[]
        {
            new Client()
            {
                ClientId="AppBasicUser",
                ClientName="Basic User",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets={new Secret("HealthCareMemberHealthCareMember".Sha256()) },
                AllowedScopes =
                {
                    "ReadPermission",
                    IdentityServerConstants.LocalApi.ScopeName,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Profile
                },
                AccessTokenLifetime = 3600
            },
            new Client()
            {
                ClientId="AppMemberUser",
                ClientName="Admin User",
                AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                ClientSecrets={new Secret("HealthCareAdminsHealthCareAdmins".Sha256()) },
                AllowOfflineAccess = true, // Refresh Token için eklenmesi gereklidir.
                RefreshTokenUsage = TokenUsage.OneTimeOnly, // Her access_token isteğinde yeni bir refresh token üretir.
                RefreshTokenExpiration = TokenExpiration.Absolute, // Refresh token süresi dolduğunda yeni refresh token üretmez.
                AbsoluteRefreshTokenLifetime = 3600 * 24 * 30, // Refresh token süresi 30 gün
                AllowedScopes =
                {
                    "TokenPermission",
                    IdentityServerConstants.LocalApi.ScopeName,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess // Refresh Token için gereklidir.
                },
                AccessTokenLifetime = 3600
            }
        };
    }
}
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Core.IdentityServer
{
    public class Config
    {
        // API'ye erişim sağlacak kaynak ve kaynağın kapsamları
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("HealthCareFullPermission")
            {
                Scopes =
                {
                    "ReadPermission",
                    "FullPermission"
                }
            }
        };

        // Kaynak tanımları
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("ReadPermission","Read Permission For Users",new[]{"scope"}),
            new ApiScope("FullPermission","Read and Write Full Permission For Users",new[]{"scopes"}),
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
                ClientId="HealthCareMember",
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
                ClientId="HealthCareAdmin",
                ClientName="Admin User",
                AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                ClientSecrets={new Secret("HealthCareAdminsHealthCareAdmins".Sha256()) },
                AllowedScopes =
                {
                    "ReadPermission",
                    "FullPermission",
                    IdentityServerConstants.LocalApi.ScopeName,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Profile
                },
                AccessTokenLifetime = 3600
            }
        };
    }
}

using Microsoft.AspNetCore.Authorization;

namespace HealthCare.Descriptions.WebAPI.Policies.DynamicPolicy
{
    public class DynamicRoleRequirement : IAuthorizationRequirement
    {
        // Authorization motoruna özel bir kural olduğunu belirtmek için kullanılır.
        // AuthorizationHandler sınıfı içinde miras alınan class'ta belirtilir.
    }
}
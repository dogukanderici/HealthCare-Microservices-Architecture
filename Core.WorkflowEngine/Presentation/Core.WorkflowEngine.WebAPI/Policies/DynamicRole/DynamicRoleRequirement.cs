using Microsoft.AspNetCore.Authorization;

namespace Core.WorkflowEngine.WebAPI.Policies.DynamicRole
{
    public class DynamicRoleRequirement : IAuthorizationRequirement
    {
        // Authorization motoruna özel bir kural olduğunu belirtmek için kullanılır.
        // AuthorizationHandler sınıfı içinde miras alınan class'ta belirtilir.
    }
}
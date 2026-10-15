using Microsoft.AspNetCore.Authorization;

namespace HealthCare.Descriptions.WebAPI.Policies.DynamicPolicy
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DynamicRoleAttribute : AuthorizeAttribute
    {
        public DynamicRoleAttribute() : base(policy: "DynamicRolePolicy")
        {
            // Marker attribute
            // Endpoint'in dinamik rol kontrolünden geçeceğini belirtir.
        }
    }
}
using Microsoft.AspNetCore.Authorization;

namespace Core.WorkflowEngine.WebAPI.Policies.DynamicRole
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DynamicRoleAttribute : AuthorizeAttribute
    {
        public DynamicRoleAttribute() : base(policy: "DynamicRolePolicy")
        {
            // Marker Attribute
            // Endpoint'in dinamik rol kontrolünden geçeceğini belirtir.
        }
    }
}
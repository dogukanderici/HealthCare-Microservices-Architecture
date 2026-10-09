using Microsoft.AspNetCore.Authorization;

namespace Core.WorkflowEngine.WebAPI.Policies.DynamicRole
{
    public class DynamicRoleHandler : AuthorizationHandler<DynamicRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DynamicRoleRequirement requirement)
        {
            throw new NotImplementedException();
        }
    }
}
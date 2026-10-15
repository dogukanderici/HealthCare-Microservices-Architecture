using HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HealthCare.Descriptions.WebAPI.Policies.DynamicPolicy
{
    public class DynamicRoleHandler : AuthorizationHandler<DynamicRoleRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;

        public DynamicRoleHandler(IHttpContextAccessor httpContextAccessor, IMediator mediator)
        {
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DynamicRoleRequirement requirement)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return;

            string httpMethode = httpContext.Request.Method;
            Endpoint endpoint = httpContext.GetEndpoint();
            string endpointKey = (endpoint as RouteEndpoint)?.RoutePattern.RawText;

            var userRoles = context.User.Claims
                .Where(x => x.Type == ClaimTypes.Role || x.Type == "role" || x.Type == @"http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                .Select(x => x.Value)
                .ToList();

            if (!userRoles.Any())
                return;

            GetDynamicRolesByFilterQuery roleQuery = GetDynamicRolesByFilterQuery.Filter(null, null, httpMethode, endpointKey, true);

            var mediatorResponse = await _mediator.Send(roleQuery);

            IReadOnlyCollection<GetDynamicRolesByFilterQueryResult> allowedRoles = mediatorResponse.Data;

            if (!allowedRoles.Any())
                return;

            var allowedRoleSet = new HashSet<string>(allowedRoles.Select(x => x.SyncedRole.Name).ToList(), StringComparer.OrdinalIgnoreCase);

            bool isAuthorized = allowedRoleSet.Overlaps(userRoles);

            if (isAuthorized)
                context.Succeed(requirement);
            else
                return;
        }
    }
}
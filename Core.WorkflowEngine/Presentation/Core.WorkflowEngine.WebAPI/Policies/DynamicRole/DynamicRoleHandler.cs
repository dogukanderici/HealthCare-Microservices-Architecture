using Core.WorkflowEngine.Application.Features.Mediator.Queries.DynamicRoleQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.DynamicRoleResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Core.WorkflowEngine.WebAPI.Policies.DynamicRole
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
            Endpoint endPoint = httpContext.GetEndpoint();
            string endPointKey = (endPoint as RouteEndpoint)?.RoutePattern.RawText;

            var userRoles = context.User.Claims
                .Where(c => c.Type == ClaimTypes.Role || c.Type == "role" || c.Type == @"http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                .Select(c => c.Value)
                .ToList();

            if (!userRoles.Any())
                return;

            GetDynamicRolesByFilterQuery query = GetDynamicRolesByFilterQuery.Filter(null, null, httpMethode, endPointKey, true);

            var mediatorResponse = await _mediator.Send(query);

            IReadOnlyCollection<GetDynamicRolesByFilterQueryResult> allowedRoles = mediatorResponse.Data;

            if (!allowedRoles.Any())
                return;

            var allowedRoleSet = new HashSet<string>(allowedRoles.Select(r => r.SyncedRole.Name).ToList(), StringComparer.OrdinalIgnoreCase);

            bool isAuthorized = allowedRoleSet.Overlaps(userRoles);

            if (isAuthorized)
                context.Succeed(requirement);
        }
    }
}
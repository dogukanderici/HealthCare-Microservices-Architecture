using Core.WorkflowEngine.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Core.WorkflowEngine.Persistence.Services.CurrentUserServices
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                return Guid.TryParse(userIdClaim, out Guid userId) ? userId : Guid.Empty;
            }
        }
        public DateTimeOffset CurrentDate { get => DateTimeOffset.UtcNow; }
    }
}
using AgileBoard.Application.Services.UserContext;
using AgileBoard.Core.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace AgileBoard.Infrastructure.Services;

internal sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public JobTitle GetCurrentUserJobTitle()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext?.User?.Identity?.IsAuthenticated != true)
        {
            // Default to BusinessAnalyst for unauthenticated users or when no role is specified
            // In production, you might want to throw an exception or handle this differently
            return JobTitle.BusinessAnalyst;
        }

        // Try to get the job title from claims
        var jobTitleClaim = httpContext.User.FindFirst("JobTitle")?.Value
                           ?? httpContext.User.FindFirst("role")?.Value;

        if (!string.IsNullOrEmpty(jobTitleClaim))
        {
            return jobTitleClaim;
        }

        // Default fallback
        return JobTitle.BusinessAnalyst;
    }
}

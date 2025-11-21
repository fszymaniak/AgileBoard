using AgileBoard.Application.Services.CurrentUserService;
using AgileBoard.Core.ValueObjects;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AgileBoard.Infrastructure.Auth;

internal sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public JobTitle GetCurrentUserJobTitle()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext?.User?.Identity?.IsAuthenticated != true)
        {
            // Default to BusinessAnalyst for backward compatibility
            // In production, this should throw an exception
            return JobTitle.BusinessAnalyst;
        }

        var jobTitleClaim = httpContext.User.FindFirst("JobTitle")?.Value;

        if (string.IsNullOrEmpty(jobTitleClaim))
        {
            // Default to BusinessAnalyst if no claim found
            return JobTitle.BusinessAnalyst;
        }

        // Parse the JobTitle from the claim
        return jobTitleClaim switch
        {
            "BusinessAnalyst" => JobTitle.BusinessAnalyst,
            "ProductOwner" => JobTitle.ProductOwner,
            "ScrumMaster" => JobTitle.ScrumMaster,
            "DevelopmentTeamMember" => JobTitle.DevelopmentTeamMember,
            _ => JobTitle.BusinessAnalyst // Default fallback
        };
    }

    public string? GetCurrentUserId()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        return httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public bool IsAuthenticated
    {
        get
        {
            var httpContext = _httpContextAccessor.HttpContext;
            return httpContext?.User?.Identity?.IsAuthenticated ?? false;
        }
    }
}

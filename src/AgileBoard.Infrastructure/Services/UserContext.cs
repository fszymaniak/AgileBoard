using AgileBoard.Application.Services.UserContext;
using AgileBoard.Core.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AgileBoard.Infrastructure.Services;

internal sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<UserContext> _logger;

    // Valid job titles for validation
    private static readonly string[] ValidJobTitles =
    {
        JobTitle.BusinessAnalyst,
        JobTitle.ProductOwner,
        JobTitle.ScrumMaster,
        JobTitle.DevelopmentTeamMember
    };

    public UserContext(IHttpContextAccessor httpContextAccessor, ILogger<UserContext> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public JobTitle GetCurrentUserJobTitle()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        // Check if user is authenticated
        if (httpContext?.User?.Identity?.IsAuthenticated != true)
        {
            _logger.LogWarning("User is not authenticated. Using default JobTitle: {JobTitle}", JobTitle.BusinessAnalyst);
            // Default to BusinessAnalyst for unauthenticated users
            // TODO: Consider throwing UnauthorizedException when authentication is enabled
            return JobTitle.BusinessAnalyst;
        }

        // Try to get the job title from claims (check both "JobTitle" and "role" claim types)
        var jobTitleClaim = httpContext.User.FindFirst("JobTitle")?.Value
                           ?? httpContext.User.FindFirst("role")?.Value;

        if (string.IsNullOrEmpty(jobTitleClaim))
        {
            _logger.LogWarning("Authenticated user does not have JobTitle or role claim. Using default JobTitle: {JobTitle}", JobTitle.BusinessAnalyst);
            // Default fallback when no claim is present
            return JobTitle.BusinessAnalyst;
        }

        // Validate that the job title is one of the known values
        if (!ValidJobTitles.Contains(jobTitleClaim))
        {
            _logger.LogError("Invalid JobTitle claim value: {JobTitleClaim}. Valid values are: {ValidJobTitles}",
                jobTitleClaim, string.Join(", ", ValidJobTitles));
            throw new InvalidOperationException($"Invalid JobTitle claim value: {jobTitleClaim}. Valid values are: {string.Join(", ", ValidJobTitles)}");
        }

        _logger.LogDebug("Retrieved JobTitle from claims: {JobTitle}", jobTitleClaim);
        return jobTitleClaim;
    }
}

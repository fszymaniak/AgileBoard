using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Application.Services.CurrentUserService;

public interface ICurrentUserService
{
    JobTitle GetCurrentUserJobTitle();
    string? GetCurrentUserId();
    bool IsAuthenticated { get; }
}

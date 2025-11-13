using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Application.Services.UserContext;

public interface IUserContext
{
    JobTitle GetCurrentUserJobTitle();
}

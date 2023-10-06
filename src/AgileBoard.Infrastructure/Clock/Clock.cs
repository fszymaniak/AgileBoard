using AgileBoard.Application.Services.Clock;

namespace AgileBoard.Infrastructure.Clock;

public class Clock : IClock
{
    public DateTimeOffset Current() => DateTimeOffset.UtcNow;
}
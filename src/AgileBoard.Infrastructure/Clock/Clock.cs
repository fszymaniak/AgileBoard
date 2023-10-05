namespace AgileBoard.Application.Services.Clock;

public class Clock : IClock
{
    public DateTimeOffset Current() => DateTimeOffset.UtcNow;
}
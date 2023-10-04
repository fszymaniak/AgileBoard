namespace AgileBoard.Api.Services.Clock;

public class Clock : IClock
{
    public DateTimeOffset Current() => DateTimeOffset.UtcNow;
}
namespace AgileBoard.Application.Services.Clock;

public interface IClock
{
    public DateTimeOffset Current();
}
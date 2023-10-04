namespace AgileBoard.Api.Services.Clock;

public interface IClock
{
    public DateTimeOffset Current();
}
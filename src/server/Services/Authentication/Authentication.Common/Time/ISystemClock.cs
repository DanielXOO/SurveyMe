namespace Authentication.Time;

public interface ISystemClock
{
    DateTime UtcNow { get; }
}
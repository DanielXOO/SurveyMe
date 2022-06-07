namespace Authentication.Api.Common.Time;

public interface ISystemClock
{
    DateTime UtcNow { get; }
}
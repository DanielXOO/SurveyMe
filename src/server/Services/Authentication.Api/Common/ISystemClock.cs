namespace Authentication.Api.Common;

public interface ISystemClock
{
    DateTime UtcNow { get; }
}
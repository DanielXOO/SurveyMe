namespace Surveys.Api.Common.Time;

public interface ISystemClock
{
    DateTime UtcNow { get; }
}
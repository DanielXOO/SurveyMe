namespace Surveys.Common.Time;

public interface ISystemClock
{
    DateTime UtcNow { get; }
}
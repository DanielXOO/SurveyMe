namespace Answers.Api.Common.Time;

public class SystemClock : ISystemClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
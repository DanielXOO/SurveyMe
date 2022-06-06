namespace Authentication.Api.Common;

public class SystemClock : ISystemClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
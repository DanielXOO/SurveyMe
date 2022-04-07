using System;

namespace iTechArt.Common.Time
{
    public interface ISystemClock
    {
        DateTime UtcNow { get; }
    }
}
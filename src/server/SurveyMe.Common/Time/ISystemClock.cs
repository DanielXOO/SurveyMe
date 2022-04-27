using System;

namespace SurveyMe.Common.Time
{
    public interface ISystemClock
    {
        DateTime UtcNow { get; }
    }
}
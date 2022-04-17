using System;

namespace SurveyMe.Common
{
    public interface ISystemClock
    {
        DateTime UtcNow { get; }
    }
}
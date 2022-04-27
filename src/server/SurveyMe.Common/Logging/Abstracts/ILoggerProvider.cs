namespace SurveyMe.Common.Logging.Abstracts
{
    public interface ILoggerProvider
    {
        ILogger CreateLogger(string name);
    }
}
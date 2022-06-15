using SurveyMe.Common.Logging.Abstracts;

namespace SurveyMe.Common.Logging;

public sealed class Logger<T> : ILogger<T>
{
    private readonly ILogger _logger;


    public Logger(ILoggerProvider logger)
    {
        var typeName = typeof(T).Name;
        _logger = logger.CreateLogger(typeName);
    }


    public void LogCritical(string message, params object[] args)
    {
        _logger.LogCritical(message, args);
    }

    public void LogCritical(Exception exception, string message, params object[] args)
    {
        _logger.LogCritical(exception, message, args);
    }

    public void LogDebug(string message, params object[] args)
    {
        _logger.LogDebug(message, args);
    }

    public void LogDebug(Exception exception, string message, params object[] args)
    {
        _logger.LogDebug(exception, message, args);
    }

    public void LogError(string message, params object[] args)
    {
        _logger.LogError(message, args);
    }

    public void LogError(Exception exception, string message, params object[] args)
    {
        _logger.LogError(exception, message, args);
    }

    public void LogInformation(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
    }

    public void LogInformation(Exception exception, string message, params object[] args)
    {
        _logger.LogInformation(exception, message, args);
    }

    public void LogTrace(string message, params object[] args)
    {
        _logger.LogTrace(message, args);
    }

    public void LogTrace(Exception exception, string message, params object[] args)
    {
        _logger.LogTrace(exception, message, args);
    }

    public void LogWarning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
    }

    public void LogWarning(Exception exception, string message, params object[] args)
    {
        _logger.LogWarning(exception, message, args);
    }
}
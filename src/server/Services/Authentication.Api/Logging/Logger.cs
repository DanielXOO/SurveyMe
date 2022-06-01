using ILogger = Authentication.Api.Logging.Abstracts.ILogger;

namespace Authentication.Api.Logging;

internal sealed class Logger : ILogger
{
    private readonly Microsoft.Extensions.Logging.ILogger _logger;


    public Logger(Microsoft.Extensions.Logging.ILogger logger)
    {
        _logger = logger;
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
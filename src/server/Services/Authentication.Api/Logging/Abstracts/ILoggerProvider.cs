namespace Authentication.Api.Logging.Abstracts;

public interface ILoggerProvider
{
    ILogger CreateLogger(string name);
}
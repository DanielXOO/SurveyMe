namespace Authentication.Logging.Abstracts;

public interface ILoggerProvider
{
    ILogger CreateLogger(string name);
}
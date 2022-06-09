using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;
using ILogger = Authentication.Logging.Abstracts.ILogger;

namespace Authentication.Logging;

internal sealed class LoggerProvider : Abstracts.ILoggerProvider
{
    private readonly ILoggerFactory _factory;


    public LoggerProvider(ILoggerFactory factory)
    {
        _factory = factory;
    }


    public ILogger CreateLogger(string name)
    {
        return new Logger(_factory.CreateLogger(name));
    }

    public void Dispose()
    {
        _factory.Dispose();
    }
}
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;
using ILogger = Authentication.Api.Logging.Abstracts.ILogger;
using ILoggerProvider = Authentication.Api.Logging.Abstracts.ILoggerProvider;

namespace Authentication.Api.Logging;

internal sealed class LoggerProvider : ILoggerProvider
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
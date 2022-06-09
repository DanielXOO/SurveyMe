using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ILoggingBuilder = Microsoft.Extensions.Logging.ILoggingBuilder;
using ILoggerProvider = Authentication.Logging.Abstracts.ILoggerProvider;

namespace Authentication.Logging;

public static class LoggerExtensions
{
    public static ILoggingBuilder AddLogger(this ILoggingBuilder builder)
    {
        builder.Services.AddSingleton(typeof(Abstracts.ILogger<>), typeof(Logger<>));
        builder.Services.AddSingleton<ILoggerProvider, LoggerProvider>();
        builder.Services.AddSingleton(sp =>
        {
            var loggerProvider = sp.GetRequiredService<ILoggerProvider>();

            return loggerProvider.CreateLogger("");
        });

        return builder;
    }
}
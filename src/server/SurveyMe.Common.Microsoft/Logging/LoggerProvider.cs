using SurveyMe.Common.Logging;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace SurveyMe.Common.Microsoft.Logging
{
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
    }
}
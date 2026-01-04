using System.Collections.Concurrent;

namespace ApiCatalogo1.Logging
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        readonly CustomLoggingProviderConfiguration loggerConfig;

        readonly ConcurrentDictionary<string, CustomerLogger> loggers = new ConcurrentDictionary<string, CustomerLogger>();

        public CustomLoggerProvider(CustomLoggingProviderConfiguration config)
        {
            loggerConfig = config;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, name => new CustomerLogger(name, loggerConfig));
        }

        public void Dispose()
        {
            loggers.Clear();
        }
    }
}

using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Xml;

namespace WebStoreLogger
{
    internal class Log4NetLoggerProvider : ILoggerProvider
    {
        private readonly string _Configuration;

        private readonly ConcurrentDictionary<string, Log4NetLogger> _Loggers = new();

        public Log4NetLoggerProvider(string Configuration)
        {
            _Configuration = Configuration;
        }

        public ILogger CreateLogger(string Category) =>
            _Loggers.GetOrAdd(Category, category =>
            {
                var xml = new XmlDocument();
                xml.Load(_Configuration);
                return new Log4NetLogger(category, xml["log4net"]);
            });

        public void Dispose() => _Loggers.Clear();
    }
}

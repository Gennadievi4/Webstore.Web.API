using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Xml;

namespace WebStoreLogger
{
    class Log4NetLogger : ILogger
    {
        private readonly ILog _Log;

        public Log4NetLogger(string Category, XmlElement Configuration)
        {
            var logger_rep = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(Hierarchy)
                );

            _Log = LogManager.GetLogger(logger_rep.Name, Category);

            XmlConfigurator.Configure(logger_rep, Configuration);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel LogLevel) => LogLevel switch
        {
            LogLevel.None => false,
            LogLevel.Trace => _Log.IsDebugEnabled,
            LogLevel.Debug => _Log.IsDebugEnabled,
            LogLevel.Information => _Log.IsInfoEnabled,
            LogLevel.Warning => _Log.IsWarnEnabled,
            LogLevel.Error => _Log.IsErrorEnabled,
            LogLevel.Critical => _Log.IsFatalEnabled,
            _ => throw new ArgumentOutOfRangeException(nameof(LogLevel), LogLevel, null)
        };

        public void Log<TState>(LogLevel LogLevel, EventId EventId, TState State, Exception Error, Func<TState, Exception, string> Formatter)
        {
            if (Formatter is null)
                throw new ArgumentNullException(nameof(Formatter));

            if (!IsEnabled(LogLevel)) return;

            var log_message = Formatter(State, Error);

            if (string.IsNullOrEmpty(log_message) && Error is null) return;

            switch (LogLevel)
            {
                case LogLevel.None: break;
                case LogLevel.Trace:
                case LogLevel.Debug:
                    _Log.Debug(log_message);
                    break;
                case LogLevel.Information:
                    _Log.Info(log_message);
                    break;
                case LogLevel.Warning:
                    _Log.Warn(log_message);
                    break;
                case LogLevel.Error:
                    _Log.Error(log_message, Error);
                    break;
                case LogLevel.Critical:
                    _Log.Fatal(log_message, Error);
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(LogLevel), LogLevel, null);
            }
        }
    }
}

namespace MessageQueue.Logging
{
    using System;

    public interface ILogger
    {
        void Debug(string message);
        void Debug(string message, Exception exception);
        void DebugFormat(string template, params object[] parameters);
        void DebugFormat(Exception exception, string template, params object[] parameters);
        void Debug(string message, DateTime timestamp);
        void Debug(string message, Exception exception, DateTime timestamp);
        void DebugFormat(string template, DateTime timestamp, params object[] parameters);
        void DebugFormat(Exception exception, string template, DateTime timestamp, params object[] parameters);

        void Info(string message);
        void Info(string message, Exception exception);
        void InfoFormat(string template, params object[] parameters);
        void InfoFormat(Exception exception, string template, params object[] parameters);
        void Info(string message, DateTime timestamp);
        void Info(string message, Exception exception, DateTime timestamp);
        void InfoFormat(string template, DateTime timestamp, params object[] parameters);
        void InfoFormat(Exception exception, string template, DateTime timestamp, params object[] parameters);

        void Warn(string message);
        void Warn(string message, Exception exception);
        void WarnFormat(string template, params object[] parameters);
        void WarnFormat(Exception exception, string template, params object[] parameters);
        void Warn(string message, DateTime timestamp);
        void Warn(string message, Exception exception, DateTime timestamp);
        void WarnFormat(string template, DateTime timestamp, params object[] parameters);
        void WarnFormat(Exception exception, string template, DateTime timestamp, params object[] parameters);

        void Error(string message);
        void Error(string message, Exception exception);
        void ErrorFormat(string template, params object[] parameters);
        void ErrorFormat(Exception exception, string template, params object[] parameters);
        void Error(string message, DateTime timestamp);
        void Error(string message, Exception exception, DateTime timestamp);
        void ErrorFormat(string template, DateTime timestamp, params object[] parameters);
        void ErrorFormat(Exception exception, string template, DateTime timestamp, params object[] parameters);
    }
}
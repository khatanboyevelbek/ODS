namespace ODS.Web.Brokers.Loggings
{
    public interface ILoggingBroker
    {
        void LogInformation(string message);
        void LogDebug(string message);
        void LogError(string message);
        void LogWarning(string message);
        void LogTrace(string message);
        void LogCritical(Exception exception);
    }
}

namespace EmergencyButton.Core.Instrumentation
{
    public interface ILogStorage
    {
        void StoreLogEntry(LogEntry logEntry);
    }
}
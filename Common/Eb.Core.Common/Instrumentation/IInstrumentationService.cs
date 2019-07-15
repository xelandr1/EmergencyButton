using System;

namespace EmergencyButton.Core.Instrumentation
{
    public interface IInstrumentationService : IDisposable
    {
        /// <summary>
        ///     Операция записи события в лог.
        /// </summary>
        /// <param name="logEntry">Запись журнала событий.</param>
        void WriteLogEntry(LogEntry logEntry);
    }
}
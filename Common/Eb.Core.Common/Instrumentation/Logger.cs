using System;
using System.Diagnostics;
using EmergencyButton.Core.Common;
using EmergencyButton.Core.ComponentModel;

namespace EmergencyButton.Core.Instrumentation
{
    public static class Logger
    {
        public static LogEntry leInformationTemplate = new LogEntry
            { SubsystemId = SubsystemIdentity.SubsystemId, Severity = TraceEventType.Information };

        public static LogEntry leWarningTemplate = new LogEntry
            { SubsystemId = SubsystemIdentity.SubsystemId, Severity = TraceEventType.Warning };

        public static LogEntry leErrorTemplate = new LogEntry
            { SubsystemId = SubsystemIdentity.SubsystemId, Severity = TraceEventType.Error };

        public static void Information(string title, string componentName, string description = "")
        {
            var logEntry = new LogEntry(leInformationTemplate)
                { Title = title, ComponentName = componentName, Description = description };
            Singleton.InstrumentationService.WriteLogEntry(logEntry);
        }

        public static void Warning(string title, string componentName, string description = "")
        {
            var logEntry = new LogEntry(leWarningTemplate)
                { Title = title, ComponentName = componentName, Description = description };
            Singleton.InstrumentationService.WriteLogEntry(logEntry);
        }

        public static void Error(string title, string componentName, string description = "")
        {
            var logEntry = new LogEntry(leErrorTemplate)
                { Title = title, ComponentName = componentName, Description = description };
            Singleton.InstrumentationService.WriteLogEntry(logEntry);
        }
        public static void Error(string title, string componentName, Exception exception=null)
        {
            var logEntry = new LogEntry(leErrorTemplate)
                { Title = title, ComponentName = componentName, Description = exception.ToString() };
            Singleton.InstrumentationService.WriteLogEntry(logEntry);
        }

    }
}
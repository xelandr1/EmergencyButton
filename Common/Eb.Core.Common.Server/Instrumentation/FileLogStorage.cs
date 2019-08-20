using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using EmergencyButton.Core.Instrumentation;

namespace EmergencyButton.Core.Server.Instrumentation
{
    public class FileLogStorage : ILogStorage
    {
        public static string _LogFileName;

        private readonly string _logFilesPath;
        private static string DefaultLogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EbLogs");
        private object _lockFileObject=new object();

        public FileLogStorage() : this(string.Empty)
        {
        }

        public FileLogStorage(string logFilesPath)
        {
            _logFilesPath = string.IsNullOrWhiteSpace(logFilesPath) ? DefaultLogFilePath : logFilesPath;
        }

        public string LogFileName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_LogFileName))
                {
                    if (!Directory.Exists(_logFilesPath))
                    {
                        lock (_lockFileObject)
                        {

                            Directory.CreateDirectory(_logFilesPath);
                        }
                    }

                    _LogFileName = Path.Combine(_logFilesPath, string.Format("{0:dd.MM.yy HH-mm}.log",
                        DateTime.Now));
                }

                return _LogFileName;
            }
        }


        public void StoreLogEntry(LogEntry logEntry)
        {
            try
            {
                lock (_lockFileObject)
                {

                    //if (!File.Exists(LogFileName))
                    //    File.Create(LogFileName);

                    var logText = new StringBuilder();
                    logText.AppendLine(InstrumentationConstants.DumpSeparateString);
                    logText.AppendLine($"{logEntry.TimeStamp} {logEntry.Severity.ToString()} {logEntry.ComponentName}");
                    if (!string.IsNullOrWhiteSpace(logEntry.Title)) logText.AppendLine(logEntry.Title);
                    if (!string.IsNullOrWhiteSpace(logEntry.Description)) logText.AppendLine(logEntry.Description);
                    if (!string.IsNullOrWhiteSpace(logEntry.Context)) logText.AppendLine(logEntry.Context);
                    logText.AppendLine(InstrumentationConstants.DumpSeparateString);
                    File.AppendAllText(LogFileName, logText.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }
        }
    }
}
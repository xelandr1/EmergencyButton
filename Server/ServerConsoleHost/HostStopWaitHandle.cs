using System;
using System.Diagnostics;
using System.Security;
using System.Security.AccessControl;
using System.Threading;

namespace ServerConsoleHost
{
    /// <summary>
    /// Хелпер для события ожидания остановки физического хоста.
    /// </summary>
    public static class HostStopWaitHandle
    {

        /// <summary>
        /// Используемый WaitHandle.
        /// </summary>
        // ReSharper disable once NotAccessedField.Local
        private static EventWaitHandle handleHolder;

        [SecuritySafeCritical]
        static HostStopWaitHandle()
        {
            handleHolder = new EventWaitHandle(false, EventResetMode.ManualReset, GetHandleName());
        }

        /// <summary>
        /// Получить имя WaitHandle.
        /// </summary>
        /// <returns>имя WaitHandle.</returns>
        [SecurityCritical]
        private static string GetHandleName()
        {
            return "8AC59B1B-3210-41DC-8BA3-A2E941A753D0:" + Process.GetCurrentProcess().Id;
        }

        /// <summary>
        /// Возвращает новый экземпляр EventWaitHandle для события ожидания остановки физического хоста
        /// </summary>
        [SecuritySafeCritical]
        private static EventWaitHandle OpenWaitHandle()
        {
            return EventWaitHandle.OpenExisting(GetHandleName());
        }

        /// <summary>
        /// Включает событие ожидания остановки физического хоста
        /// </summary>
        public static void Set()
        {
            using (var handle = OpenWaitHandle())
            {
                handle.Set();
            }
        }

        /// <summary>
        /// Ожидает включения события ожидания остановки физического хоста
        /// </summary>
        public static void Wait()
        {
            var waitingThread = new Thread(WaitingThread)
            {
                Name = "SetWaitForConsoleReadLineStrategy_Waiting",
                IsBackground = true,
            };

            waitingThread.Start();

            using (var handle = OpenWaitHandle())
            {
                handle.WaitOne();
            }
        }

        /// <summary>
        /// Реализация тела ожидающего потока.
        /// </summary>
        private static void WaitingThread()
        {
            Console.WriteLine("\nWaiting...");

            var explicitQuit = false;
            var exit = false;
            while (!exit)
            {
                string cmd = Console.ReadLine();

                switch (cmd)
                {
                    case "nq":
                    {
                        if (!explicitQuit)
                        {
                            Console.WriteLine("Explicit quit mode set to On. Enter q to quit.");
                            explicitQuit = true;
                        }
                    }
                        break;
                    case "q":
                    {
                        exit = true;
                    }
                        break;
                    case "gc":
                    {
                        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                        int maxGenCollectCount = GC.CollectionCount(GC.MaxGeneration);
                        long totalUsedMemory = GC.GetTotalMemory(false);
                        Console.WriteLine("CLR memory used: {0}. MaxGeneration collect count: {1}", totalUsedMemory, maxGenCollectCount);
                    }
                        break;
                    default:
                    {
                        if (!explicitQuit)
                            exit = true;
                    }
                        break;
                }
            }

            Set();
        }

    }
}
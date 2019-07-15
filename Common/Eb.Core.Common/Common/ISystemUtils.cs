using System;
using EmergencyButton.Core.Common;

namespace EmergencyButton.Core.Common
{
    public interface ISystemUtils
    {
        string CurrentEbVersion { get; }
        void Sleep(int ms, SafeCancellationToken cancelToken);

        SafeCancellationToken StartTimer(Action<SafeCancellationToken> tick, Func<int> needInterval,
            bool startImmidiate = true, bool ticksSuperposition = false);
    }
}
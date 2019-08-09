using System;

namespace EmergencyButton.Core.ComponentModel.Event
{
    /// <summary>
    /// Типизорованная обертка EventArgs
    /// </summary>
    /// <typeparam name="TEventContext">рабочий тип</typeparam>
    public class TEventArgs<TEventContext> : EventArgs
    {
        public TEventArgs(TEventContext eventContext)
        {
            EventContext = eventContext;
        }

        public TEventContext EventContext { get; private set; }
    }
}
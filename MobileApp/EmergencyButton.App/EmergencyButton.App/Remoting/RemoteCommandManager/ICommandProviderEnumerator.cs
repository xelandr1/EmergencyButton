using System;
using System.Collections.Generic;
using EmergencyButton.Core.Common;

namespace EmergencyButton.App.Remote
{
    public interface ICommandProviderEnumerator
    {
        IList<Type> CommandProviders { get; }
    }
}
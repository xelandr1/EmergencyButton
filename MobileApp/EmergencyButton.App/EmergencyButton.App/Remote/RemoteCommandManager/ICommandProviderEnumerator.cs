using System;
using System.Collections.Generic;
using EmergencyButton.Core.Common;

namespace EmergencyButton.App.Remote
{
    public interface ICommandProviderEnumerator
    {
        /// <summary>
        /// list of command providers. <baseTransportLayerMode for this provider, clrType of provider>
        /// </summary>
        IList<PairValue<TransportLayerMode, Type>> CommandProviders { get; }
    }
}
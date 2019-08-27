using System;
using System.Collections.Generic;
using EmergencyButton.App.Remote.Http;

namespace EmergencyButton.App.Remote
{
    public class DefaultCommandProviderEnumerator: ICommandProviderEnumerator
    {
        public IList<Type> CommandProviders =>
            new List<Type>()
            {
                typeof(HttpRemoteCommandProvider)
            };
    }
}
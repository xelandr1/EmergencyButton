using System.Collections.Specialized;

namespace EmergencyButton.Core.Configuration
{
    public interface IConfigurable
    {
        string GetConfigurationPath();
        void Configure(object configuration);
    }
}
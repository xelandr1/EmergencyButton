using Android.Content;
using EmergencyButton.App.Droid.Common;
using EmergencyButton.App.Droid.Instrumentation;
using EmergencyButton.App.Service;
using EmergencyButton.Core.ComponentModel;
using EmergencyButton.Core.Data;
using EmergencyButton.Core.Instrumentation;

namespace EmergencyButton.App.Droid.Services
{
    public static class SingletonServicesInitializer
    {
        public static void Initialize(Context currentContext)
        {
            if (!Singleton.Services.ContainService<ICurrentContext>())
                Singleton.Services.RegisterService<ICurrentContext>(new CurrentContextService(currentContext));

            if (Singleton.InstrumentationService == null)
                Singleton.Services.RegisterService<IInstrumentationService>(new InstrumentationService());

            if (!Singleton.Services.ContainService<DataEncryptor>())
                Singleton.Services.RegisterService<DataEncryptor>(new DroidDataEncryptor());

            if (!Singleton.Services.ContainService<IDataManager>())
                Singleton.Services.RegisterService<IDataManager>(new JsonFileManager());

            if (!Singleton.Services.ContainService<StoredPropertiesManager>())
                Singleton.Services.RegisterService<StoredPropertiesManager>(new StoredPropertiesManager());

            if (!Singleton.Services.ContainService<IRuntimePermissionsHandler>())
                Singleton.Services.RegisterService<IRuntimePermissionsHandler>(new RuntimePermissionsHandler());


        }
    }
}
using Android.Content;
using EmergencyButton.App.Droid.Common;
using EmergencyButton.App.Droid.Instrumentation;
using EmergencyButton.App.Remote;
using EmergencyButton.App.Remote.Http;
using EmergencyButton.App.Service;
using EmergencyButton.Core.ComponentModel;
using EmergencyButton.Core.Data;
using EmergencyButton.Core.Geolocation;
using EmergencyButton.Core.Instrumentation;

namespace EmergencyButton.App.Droid.Services
{
    public static class SingletonInitializer
    {
        public static void Initialize(Context currentContext)
        {
            if (Singleton.InstrumentationService == null)
                Singleton.Services.RegisterService<IInstrumentationService>(new DroidInstrumentationService());

            if (!Singleton.Services.ContainService<ICurrentContext>())
                Singleton.Services.RegisterService<ICurrentContext>(new CurrentContextService(currentContext));
            
            if (!Singleton.Services.ContainService<DataEncryptor>())
                Singleton.Services.RegisterService<DataEncryptor>(new DroidDataEncryptor());

            if (!Singleton.Services.ContainService<IDataManager>())
                Singleton.Services.RegisterService<IDataManager>(new JsonFileManager());

            if (!Singleton.Services.ContainService<StoredPropertiesManager>())
                Singleton.Services.RegisterService<StoredPropertiesManager>(new StoredPropertiesManager());

            if (!Singleton.Services.ContainService<IRuntimePermissionsHandler>())
                Singleton.Services.RegisterService<IRuntimePermissionsHandler>(new RuntimePermissionsHandler());

            if (!Singleton.Services.ContainService<IPowerManager>())
            {
                Singleton.Services.RegisterService<IPowerManager>(new DroidPowerManager());
                Singleton.Services.GetService<IPowerManager>().Activate();
            }
            if (!Singleton.Services.ContainService<IGeolocationService>())
            {
                Singleton.Services.RegisterService<IGeolocationService>(new GeolocationService());
                Singleton.Services.GetService<IGeolocationService>().Activate();
            }

            //if (!Singleton.Services.ContainService<IRemoteCommandManager>())
            //{
            //    Singleton.Services.RegisterService<IRemoteCommandManager>(new RemoteCommandManager(new DefaultCommandProviderEnumerator()));
            //    Singleton.Services.GetService<IRemoteCommandManager>().Activate();
            //}



        }

        public static void UnInitialize()
        {
            if (!Singleton.Services.ContainService<IPowerManager>())
            {
                Singleton.Services.GetService<IPowerManager>().Deactivate();
                Singleton.Services.UnRegisterService<IPowerManager>();

            }
        }
    }
}
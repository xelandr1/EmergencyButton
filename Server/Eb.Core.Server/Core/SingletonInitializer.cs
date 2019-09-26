//using Eb.Core.Server.Instrumentation;
//using EmergencyButton.Core.ComponentModel;
//using EmergencyButton.Core.Instrumentation;
//using Microsoft.Extensions.DependencyInjection;

//namespace Eb.Core.Server.Core
//{
//    public static class SingletonInitializer
//    {
//        public static void Initialize()
//        {
//            if (Singleton.InstrumentationService == null)
//                Singleton.Services.AddSingleton<IInstrumentationService>(new ServerInstrumentationService());

//        }

//        public static void TearDown()
//        {
//        }
//    }
//}
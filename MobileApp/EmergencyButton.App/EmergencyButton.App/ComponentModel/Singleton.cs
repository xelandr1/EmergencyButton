using System;
using EmergencyButton.Core.Instrumentation;
using Microsoft.Extensions.DependencyInjection;

namespace EmergencyButton.App.ComponentModel
{
    public static class Singleton
    {
        static Singleton()
        {
            //Services = new ServiceProvider(null);
        }

        public static IServiceCollection Services { get; }
        public static IServiceProvider ServiceProvider { get; }


        public static IInstrumentationService InstrumentationService => ServiceProvider.GetService<IInstrumentationService>();

        public static TService GetService<TService>()
        {
            return ServiceProvider.GetService<TService>();
        }
    }
}
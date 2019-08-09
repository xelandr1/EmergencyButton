using System;
using Android.Content;
using Android.OS;
using EmergencyButton.App.Service;
using EmergencyButton.Core.ComponentModel;
using EmergencyButton.Core.ComponentModel.Service;
using Xamarin.Essentials;

namespace EmergencyButton.App.Droid.Services
{
    public class DroidPowerManager: AbstractService, IPowerManager
    {
        private PowerManager.WakeLock _wakelock;

        private PowerManager PowerManager => Singleton.GetService<ICurrentContext>()
            .Context.GetSystemService(Context.PowerService) as PowerManager;

        //public ServiceState ServiceState { get; protected set; }
        public override void Activate()
        {
            if (ServiceState > ServiceState.None) return;
            ServiceState = ServiceState.Initiation;

            _wakelock = PowerManager.NewWakeLock(WakeLockFlags.Partial, "emergencyButton::servicewakelock");
            _wakelock.SetReferenceCounted(false);
            ServiceState = ServiceState.Active;
        }

        public override void Deactivate()
        {
            if (ServiceState >= ServiceState.Termination) return;
            ServiceState = ServiceState.Termination;
            _wakelock?.Release();
            _wakelock = null;
            ServiceState = ServiceState.Stoped;
        }

        public DevicePowerMode CurrentPowerMode { get; set; }
        public event EventHandler<DevicePowerMode> CurrentPowerModeChanged;

        protected virtual void OnCurrentPowerModeChanged(DevicePowerMode obj)
        {
            CurrentPowerModeChanged?.Invoke(this,obj);
        }

        public double BatteryChargeLevel {
            get { return Battery.ChargeLevel; }
        }

        public EmergencyButton.App.Service.BatteryState BatteryState
        {
            get
            {
                switch (Battery.State)
                {
                    case Xamarin.Essentials.BatteryState.Charging: return EmergencyButton.App.Service.BatteryState.Charging;
                    case Xamarin.Essentials.BatteryState.NotCharging: return EmergencyButton.App.Service.BatteryState.NotCharging;
                    case Xamarin.Essentials.BatteryState.Full: return EmergencyButton.App.Service.BatteryState.Full;
                    default: return EmergencyButton.App.Service.BatteryState.Unknown;
                }
            }

        }
    }
}
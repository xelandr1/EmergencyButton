using System;
using Android.App;
using Android.Content;
using Android.OS;
using EmergencyButton.App.Droid.EbService;
using EmergencyButton.App.Service;
using EmergencyButton.Core.ComponentModel;
using EmergencyButton.Core.ComponentModel.Service;
using EmergencyButton.Core.Instrumentation;
using Xamarin.Essentials;

namespace EmergencyButton.App.Droid.Services
{
    public class DroidPowerManager: AbstractService, IPowerManager
    {
        private PowerManager.WakeLock _wakelock;
        private PowerModeReceiver _powerModeReceiver;

        private PowerManager PowerManager => Singleton.GetService<ICurrentContext>()
            .Context.GetSystemService(Context.PowerService) as PowerManager;

        //public ServiceState ServiceState { get; protected set; }
        public override void Activate()
        {
            Logger.Information("Activate()", nameof(DroidPowerManager));

            if (ServiceState > ServiceState.None) return;
            ServiceState = ServiceState.Initiation;
            _wakelock = PowerManager.NewWakeLock(WakeLockFlags.Partial, "emergencyButton::servicewakelock");
            _wakelock.SetReferenceCounted(false);

            if(_powerModeReceiver==null)
            Singleton.GetService<ICurrentContext>().Context.RegisterReceiver(
                _powerModeReceiver = new PowerModeReceiver(),
                new IntentFilter(PowerManager.ActionPowerSaveModeChanged));

            ServiceState = ServiceState.Active;
        }

        public override void Deactivate()
        {
            Logger.Information("Deactivate()", nameof(DroidPowerManager));

            if (ServiceState >= ServiceState.Termination) return;
            ServiceState = ServiceState.Termination;
            _wakelock?.Release();
            _wakelock = null;

            if (_powerModeReceiver != null)
            {
                Singleton.GetService<ICurrentContext>().Context.UnregisterReceiver(_powerModeReceiver);
                _powerModeReceiver = null;
            }

            ServiceState = ServiceState.Stoped;
        }

        public DevicePowerMode CurrentPowerMode {
            get
            {
                if (PowerManager.IsPowerSaveMode) return DevicePowerMode.PowerSave;
                else if (PowerManager.IsInteractive) return DevicePowerMode.MaxPerformance;
                else if (PowerManager.IsDeviceIdleMode) return DevicePowerMode.Sleeping;
                else return DevicePowerMode.Normal;
            }
            set
            {
                switch (value)
                {
                    case DevicePowerMode.Sleeping:
                    case DevicePowerMode.PowerSave:
                        PowerManager.GoToSleep(0);
                        break;
                    case DevicePowerMode.MaxPerformance:
                    case DevicePowerMode.Normal:
                        PowerManager.WakeUp(0);
                        break;
                }
            }
        }
        public event EventHandler<DevicePowerMode> CurrentPowerModeChanged;

        internal virtual void OnCurrentPowerModeChanged()
        {
            CurrentPowerModeChanged?.Invoke(this,CurrentPowerMode);
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

    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] {PowerManager.ActionPowerSaveModeChanged})]
    public class PowerModeReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Logger.Information("OnReceive()", nameof(PowerModeReceiver));

            try
            {
                ((DroidPowerManager)Singleton.GetService<IPowerManager>()).OnCurrentPowerModeChanged();
            }
            catch
            {
                // Do nothing, just try to start service on boot completed
            }
        }
    }

}
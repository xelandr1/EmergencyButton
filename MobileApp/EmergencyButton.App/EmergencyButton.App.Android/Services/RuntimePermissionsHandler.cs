using Android;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using EmergencyButton.Core.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmergencyButton.App.ComponentModel;
using EmergencyButton.App.Service;
using EmergencyButton.Core.Instrumentation;
using Xamarin.Forms.Platform.Android;

namespace EmergencyButton.App.Droid.Services
{
    public class RuntimePermissionsHandler : IRuntimePermissionsHandler
    {
        private static readonly string[] StatGpsPermissionsIds = new[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation };
        private static readonly string[] StatAutoStartPermissionsIds = new[] { Manifest.Permission.ReceiveBootCompleted };
        private static readonly string[] StatWakeLockPermissionsIds = new[] { Manifest.Permission.WakeLock };
        private static readonly string[] StatReadWriteExternalStoragePermissionsIds = new[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage };
        private static readonly string[] StatPhoneStatePermissionsIds = new[] { Manifest.Permission.ReadPhoneState };
       // private static readonly string[] StatStartForegroundServicePermissionsIds = new[] { Manifest.Permission.ser };
       // private static readonly string[] StatStartBackgroundServicePermissionsIds = new[] { Manifest.Permission.ForegroundService };

        private readonly Dictionary<string[], ushort> _requiredPermissions = new Dictionary<string[], ushort>() {
            { StatGpsPermissionsIds, 0 },
            { StatAutoStartPermissionsIds, 1 },
            { StatWakeLockPermissionsIds, 2 },
            { StatReadWriteExternalStoragePermissionsIds, 3 },
            { StatPhoneStatePermissionsIds, 4 },
   //         { StatStartForegroundServicePermissionsIds, 5 },
        };

        public string[] GpsPermissionsIds => StatGpsPermissionsIds;
        public string[] AutoStartPermissionsIds => StatAutoStartPermissionsIds;
        public string[] WakeLockPermissionsIds => StatWakeLockPermissionsIds;
        public string[] ReadWriteExternalStoragePermissionsIds => StatReadWriteExternalStoragePermissionsIds;
        public string[] PhoneStatePermissionsIds => StatPhoneStatePermissionsIds;
   //     public string[] StartForegroundServicePermissionsIds => StatStartForegroundServicePermissionsIds;

        private readonly Dictionary<int, Action<Dictionary<string, bool>>> _callbacks = new Dictionary<int, Action<Dictionary<string, bool>>>();

        public bool IsPermissionGranted(string[] permissionsIds)
        {
            return permissionsIds.All(x =>
                ContextCompat.CheckSelfPermission(Singleton.GetService<ICurrentContext>().Context, x)
                == Android.Content.PM.Permission.Granted);
        }

        public void ResolvePermissionCallback(int permissionHashCode, Dictionary<string, bool> grantList)
        {
            if (_callbacks.ContainsKey(permissionHashCode))
            {
                var callback = _callbacks[permissionHashCode];
                callback(grantList);
                _callbacks.Remove(permissionHashCode);
            }
        }

        public async Task<Dictionary<string, bool>> TryGrantPermissions(string[] permissionsIds)
        {
            var completionSource = new TaskCompletionSource<Dictionary<string, bool>>();
            var code = _requiredPermissions[permissionsIds];
            ActivityCompat.RequestPermissions(
                Singleton.GetService<ICurrentContext>().Context.GetActivity()
                , permissionsIds, code);
            _callbacks.Add(code, (res) => completionSource.SetResult(res));
            return await completionSource.Task;
        }

        public async Task TryGrantRequiredPermissions()
        {
            try
            {
                foreach (var reqPerm in _requiredPermissions)
                {
                    if (!IsPermissionGranted(reqPerm.Key))
                    {
                        await TryGrantPermissions(reqPerm.Key);
                    }

                }
                //var permissionsHandler = this;

                //if (!permissionsHandler.IsPermissionGranted(permissionsHandler.ReadWriteExternalStoragePermissionsIds))
                //{
                //    await permissionsHandler.TryGrantPermissions(permissionsHandler.ReadWriteExternalStoragePermissionsIds);
                //}
                //if (!permissionsHandler.IsPermissionGranted(permissionsHandler.AutoStartPermissionsIds))
                //{
                //    await permissionsHandler.TryGrantPermissions(permissionsHandler.AutoStartPermissionsIds);
                //}
                //if (!permissionsHandler.IsPermissionGranted(permissionsHandler.StartForegroundServicePermissionsIds))
                //{
                //    await permissionsHandler.TryGrantPermissions(permissionsHandler.StartForegroundServicePermissionsIds);
                //}


                //var permissionsHandler = this;

                //if (!permissionsHandler.IsPermissionGranted(permissionsHandler.ReadWriteExternalStoragePermissionsIds))
                //{
                //    await permissionsHandler.TryGrantPermissions(permissionsHandler.ReadWriteExternalStoragePermissionsIds);
                //}
                //if (!permissionsHandler.IsPermissionGranted(permissionsHandler.AutoStartPermissionsIds))
                //{
                //    await permissionsHandler.TryGrantPermissions(permissionsHandler.AutoStartPermissionsIds);
                //}
                //if (!permissionsHandler.IsPermissionGranted(permissionsHandler.StartForegroundServicePermissionsIds))
                //{
                //    await permissionsHandler.TryGrantPermissions(permissionsHandler.StartForegroundServicePermissionsIds);
                //}


                //if (!permissionsHandler.IsPermissionGranted(permissionsHandler.GpsPermissionsIds))
                //{
                //    await permissionsHandler.TryGrantPermissions(permissionsHandler.GpsPermissionsIds);
                //}

                //if (!permissionsHandler.IsPermissionGranted(permissionsHandler.AutoStartPermissionsIds))
                //{
                //    await permissionsHandler.TryGrantPermissions(permissionsHandler.AutoStartPermissionsIds);
                //}

                //if (!permissionsHandler.IsPermissionGranted(permissionsHandler.PhoneStatePermissionsIds))
                //{
                //    await permissionsHandler.TryGrantPermissions(permissionsHandler.PhoneStatePermissionsIds);
                //}

                //if (!permissionsHandler.IsPermissionGranted(permissionsHandler.WakeLockPermissionsIds))
                //{
                //    await permissionsHandler.TryGrantPermissions(permissionsHandler.WakeLockPermissionsIds);
                //}

                //if (!permissionsHandler.IsPermissionGranted(permissionsHandler.StartForegroundServicePermissionsIds))
                //{
                //    await permissionsHandler.TryGrantPermissions(permissionsHandler.StartForegroundServicePermissionsIds);
                //}
            }
            catch (Exception e)
            {
                Logger.Error("Error while permisions request", nameof(RuntimePermissionsHandler), e);
            }
        }



    }



}
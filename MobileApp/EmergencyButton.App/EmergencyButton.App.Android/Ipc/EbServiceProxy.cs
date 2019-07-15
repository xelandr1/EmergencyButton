using Android.Content;
using Android.Content.PM;
using Android.Support.V4.Content;
using System;
using EmergencyButton.App.Droid.Common;
using EmergencyButton.App.Droid.EbService;

namespace EmergencyButton.App.Droid.Ipc
{
    public class EbServiceProxy:IEmergencyButtonService
    {
        public bool IsConnected { get; private set; }

        WeakReference<Context> _context;
        Context Context
        {
            get
            {
                Context ma;

                if (_context.TryGetTarget(out ma))
                {
                    return ma;
                }
                return null;
            }
        }

        public void FuckUpServiceConnection()
        {
            if (!this.IsServiceInstallCorrectly)
            {
                throw new Exception("EmergencyButton.Service не установлен");
            }

            if (this.HasPermissionToRunEbService())
            {
            }


        }


            public EbServiceProxy(Context context)
        {
            IsConnected = false;
            _context = new WeakReference<Context>(context);
        }



        public bool IsServiceInstallCorrectly
        {
            get
            {
                var cn = new ComponentName(Constants.EbServicePackageName, Constants.EbServiceComponentName);
                var serviceToStart = new Intent();
                serviceToStart.SetComponent(cn);

                var list = Context.PackageManager.QueryIntentServices(serviceToStart,
                    Android.Content.PM.PackageInfoFlags.MatchDefaultOnly);
                return list.Count > 0;

            }
        }

        public  bool HasPermissionToRunEbService()
        {
            Permission permissionCheck = ContextCompat.CheckSelfPermission(Context, Constants.EbServicePermission);
            return permissionCheck == Permission.Granted;
        }

        public string TestCall(string message)
        {
            throw new NotImplementedException();
        }
    }
}
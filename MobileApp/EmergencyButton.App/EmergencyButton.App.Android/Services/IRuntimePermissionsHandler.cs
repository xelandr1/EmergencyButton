using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmergencyButton.App.Droid.Services
{
    public interface IRuntimePermissionsHandler
    {
        bool IsPermissionGranted(string[] permissionsids);

        Task<Dictionary<string, bool>> TryGrantPermissions(string[] permissionsids);

        void ResolvePermissionCallback(int code, Dictionary<string, bool> grantList);

    }
}
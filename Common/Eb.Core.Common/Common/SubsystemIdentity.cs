using System;
using EmergencyButton.Core.ComponentModel;
using EmergencyButton.Core.Data;
using Lazurite.Data;

namespace EmergencyButton.Core.Common
{
    public static class SubsystemIdentity
    {
        private static IDataManager DataManager
        {
            get { return _dataManager; }
        } 
        private static string _subsystemId;
        private static string _instanceId;
        private static IDataManager _dataManager;

        public static string SubsystemId {
            get
            {
                if (string.IsNullOrEmpty(_subsystemId))
                {
                    return "Non";
                }


                return _subsystemId;
            }
            set
            {
                _subsystemId = value;
            }
        }

            /// <summary>
        /// Unique identificator for current Subsystem instance
        /// </summary>
        public static string InstanceId
        {
            get
            {
                if (string.IsNullOrEmpty(_instanceId))
                {
                    if (DataManager.Has(Constants.InstanceIdParamName))
                    {
                        _instanceId = DataManager.Get<string>(Constants.InstanceIdParamName);
                    }
                    else
                    {
                        _instanceId = Guid.NewGuid().ToString();
                        DataManager.Set(Constants.InstanceIdParamName, _instanceId);
                    }
                }
                return _instanceId;
            }
            set
            {
                _instanceId = value;
                DataManager.Set(Constants.InstanceIdParamName, _instanceId);
            }

        }
    }
}
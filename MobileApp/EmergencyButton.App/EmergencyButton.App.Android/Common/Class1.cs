using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EmergencyButton.App.Service;

namespace EmergencyButton.App.Droid.Common
{
   public class Stub:IStub
    {
        public void StartService()
        {
            throw new NotImplementedException();
        }

        public void ServiceInvokeTest()
        {
            throw new NotImplementedException();
        }
    }
}
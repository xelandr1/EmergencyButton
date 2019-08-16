using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmergencyButton.App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage1 : ContentPage
	{
		public MapPage1 ()
		{
			InitializeComponent ();
		}
        private void Test_OnClicked(object sender, EventArgs e)
        {
        //    Navigation.PushModalAsync(new MapPage1());
            //Singleton.GetService<IStub>().ServiceInvokeTest();
        }

    }
}
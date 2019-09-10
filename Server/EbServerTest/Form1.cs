using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmergencyButton.Core.Remoting;
using SimpleRemoteMethods.Bases;
using SimpleRemoteMethods.ClientSide;

namespace EbServerTest
{
    public partial class Form1 : Form
    {
        private Client _srmClient;

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var config = new ConnectionConfiguration();

            _srmClient = new Client(config.Host, config.Port, false, config.SecretKey, "login", "password", TimeSpan.FromSeconds(config.ConnectionTimeout));

            _srmClient.ConnectionError += Client_ConnectionError;
            _srmClient.ConnectionNormal += Client_ConnectionNormal;

        }

        private void Client_ConnectionNormal(object sender, Client e)
        {

        }

        private void Client_ConnectionError(object sender, TaggedEventArgs<RemoteException> e)
        {
            lblRes.Text = e.Target.Message;
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
           var res= await _srmClient.CallMethod<string>(RemotingConstant.CheckAvailabilityMethodName, null);

           lblRes.Text = res;
        }
    }
}

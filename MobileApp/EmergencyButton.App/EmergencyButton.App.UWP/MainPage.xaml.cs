namespace EmergencyButton.App.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoadApplication(new EmergencyButton.App.App());
        }
    }
}
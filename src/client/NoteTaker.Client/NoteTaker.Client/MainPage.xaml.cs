using System.ComponentModel;
using NoteTaker.Client.Navigation;
using NoteTaker.Client.Views;
using Xamarin.Forms;

namespace NoteTaker.Client
{
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;
            PageNavigator.AddListener(this);
            PageNavigator.AddLoginPage<LoginPage>();
        }

        protected override bool OnBackButtonPressed()
        {
            PageNavigator.Back();
            return true;
        }
    }
}

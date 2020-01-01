using System.ComponentModel;
using NoteTaker.Client.State;
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
        }

        protected override bool OnBackButtonPressed()
        {
            PageNavigator.Back();
            return true;
        }
    }
}

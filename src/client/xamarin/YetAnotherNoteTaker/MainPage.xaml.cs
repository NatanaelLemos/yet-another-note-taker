using System.ComponentModel;

using Xamarin.Forms;
using YetAnotherNoteTaker.Helpers;
using YetAnotherNoteTaker.State;

namespace YetAnotherNoteTaker
{
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            if (EnvironmentHelpers.EnvironmentName == EnvironmentName.Mac)
            {
                MasterBehavior = MasterBehavior.Split;
            }
            else
            {
                MasterBehavior = MasterBehavior.Popover;
            }

            PageNavigator.AddListener(this);
        }
    }
}

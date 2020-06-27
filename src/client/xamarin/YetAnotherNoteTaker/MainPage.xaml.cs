using System.ComponentModel;
using NLemos.Xamarin.Common.Helpers;
using NLemos.Xamarin.Common.State;
using Xamarin.Forms;
using YetAnotherNoteTaker.Blazor.State;

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
                //Collapsable panel is not currently supported by Xamarin on Mac.
                MasterBehavior = MasterBehavior.Split;
            }
            else
            {
                MasterBehavior = MasterBehavior.Popover;
            }

            ServiceLocator.Get<IPageNavigator>().AddListener(this);
        }
    }
}

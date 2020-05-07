using System.ComponentModel;

using Xamarin.Forms;
using YetAnotherNoteTaker.State;

namespace YetAnotherNoteTaker
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
    }
}

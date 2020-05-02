using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YetAnotherNoteTaker.Events;
using YetAnotherNoteTaker.Events.SettingsEvents;
using YetAnotherNoteTaker.Extensions;
using YetAnotherNoteTaker.State;

namespace YetAnotherNoteTaker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private readonly IEventBroker _eventBroker;

        public SettingsPage()
        {
            InitializeComponent();
            boxSettings.SetDynamicWidth();

            _eventBroker = ServiceLocator.Get<IEventBroker>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _eventBroker.Subscribe<SettingsQueryResult>(SettingsQueryResultHandler);
            _eventBroker.Notify(new SettingsQuery());
        }

        private Task SettingsQueryResultHandler(SettingsQueryResult arg)
        {
            swtDarkMode.IsToggled = arg?.Settings?.IsDarkMode ?? false;
            return Task.CompletedTask;
        }

        private async void btnSave_OnClick(object sender, EventArgs e)
        {
            await _eventBroker.Notify(new EditSettingsCommand(swtDarkMode.IsToggled));
            await _eventBroker.Notify(new SettingsRefreshQuery());
        }

        private void btnCancel_OnClick(object sender, EventArgs e)
        {
            PageNavigator.Back();
        }
    }
}

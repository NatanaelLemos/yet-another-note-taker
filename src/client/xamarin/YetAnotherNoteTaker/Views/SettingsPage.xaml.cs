using System;
using System.Threading.Tasks;
using NLemos.Xamarin.Common.Extensions;
using NLemos.Xamarin.Common.State;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YetAnotherNoteTaker.Blazor.State;
using YetAnotherNoteTaker.Client.Common.Events;
using YetAnotherNoteTaker.Client.Common.Events.SettingsEvents;
using N2tl.Observer;

namespace YetAnotherNoteTaker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private readonly IEventBroker _eventBroker;
        private readonly IPageNavigator _pageNavigator;

        public SettingsPage()
        {
            InitializeComponent();
            boxSettings.SetDynamicWidth();

            _eventBroker = ServiceLocator.Get<IEventBroker>();
            _pageNavigator = ServiceLocator.Get<IPageNavigator>();
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

        private async void btnCancel_OnClick(object sender, EventArgs e)
        {
            await _pageNavigator.Back();
        }
    }
}

using NoteTaker.Client.Extensions;
using NoteTaker.Client.State;
using NoteTaker.Client.State.SettingsEvents;
using NoteTaker.Domain.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTaker.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private IEventBroker _eventBroker;
        private Settings _settings;

        public SettingsPage()
        {
            InitializeComponent();
            boxSettings.SetDynamicWidth();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            _eventBroker = ServiceLocator.Get<IEventBroker>();
            _settings = await _eventBroker.Query<SettingsQuery, Settings>(new SettingsQuery());
            swtDarkMode.IsToggled = _settings.DarkMode;
            swtDarkMode.PropertyChanged += SwtDarkMode_PropertyChanged;
        }

        private async void SwtDarkMode_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (_settings.DarkMode == swtDarkMode.IsToggled)
            {
                return;
            }

            swtDarkMode.PropertyChanged -= SwtDarkMode_PropertyChanged;
            _settings.DarkMode = swtDarkMode.IsToggled;
            await _eventBroker.Command(new CreateOrUpdateSettingsCommand(_settings));
            swtDarkMode.PropertyChanged += SwtDarkMode_PropertyChanged;
        }
    }
}

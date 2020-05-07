using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YetAnotherNoteTaker.Events;
using YetAnotherNoteTaker.Events.AuthEvents;
using YetAnotherNoteTaker.Events.SettingsEvents;
using YetAnotherNoteTaker.Extensions;
using YetAnotherNoteTaker.Helpers;
using YetAnotherNoteTaker.State;

namespace YetAnotherNoteTaker.Views
{
    [AllowAnonymous]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly IEventBroker _eventBroker;

        public LoginPage()
        {
            InitializeComponent();
            _eventBroker = ServiceLocator.Get<IEventBroker>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (UserState.IsAuthenticated())
            {
                PageNavigator.NavigateTo<NotesPage>();
                return;
            }

            boxLogin.SetDynamicWidth();
            BackgroundImageSource = FileHelpers.ReadAsImageSource("YetAnotherNoteTaker.Assets.loginbg.jpg");

            txtEmail.Focus();
            txtEmail.Completed += (object sender, EventArgs e) => txtPassword.Focus();
            txtPassword.Completed += (object sender, EventArgs e) => btnLogin_OnClick(sender, e);
        }

        private async void btnLogin_OnClick(object sender, EventArgs e)
        {
            try
            {
                await _eventBroker.Notify(new LoginCommand(txtEmail.Text, txtPassword.Text));
                await _eventBroker.Notify(new SettingsRefreshQuery());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Failed to login", ex.Message, "Ok");
            }
        }

        private void btnRegister_OnClick(object sender, EventArgs e)
        {
            PageNavigator.NavigateTo<RegisterPage>();
        }
    }
}

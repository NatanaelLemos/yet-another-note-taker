using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YetAnotherNoteTaker.Client.Common.Events;
using YetAnotherNoteTaker.Client.Common.Events.AuthEvents;
using YetAnotherNoteTaker.Client.Common.Events.SettingsEvents;
using YetAnotherNoteTaker.Client.Common.Security;
using YetAnotherNoteTaker.Client.Common.State;
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
        private readonly IUserState _userState;

        public LoginPage()
        {
            InitializeComponent();
            _eventBroker = ServiceLocator.Get<IEventBroker>();
            _userState = ServiceLocator.Get<IUserState>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (await _userState.IsAuthenticated())
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

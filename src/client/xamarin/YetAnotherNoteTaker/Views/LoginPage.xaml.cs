using System;
using NLemos.Xamarin.Common.Extensions;
using NLemos.Xamarin.Common.Helpers;
using NLemos.Xamarin.Common.State;
using Nxl.Observer;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YetAnotherNoteTaker.Blazor.State;
using YetAnotherNoteTaker.Client.Common.Events.AuthEvents;
using YetAnotherNoteTaker.Client.Common.Events.SettingsEvents;
using YetAnotherNoteTaker.Client.Common.Security;
using YetAnotherNoteTaker.Client.Common.State;

namespace YetAnotherNoteTaker.Views
{
    [AllowAnonymous]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly IEventBroker _eventBroker;
        private readonly IUserState _userState;
        private readonly IPageNavigator _pageNavigator;

        public LoginPage()
        {
            InitializeComponent();
            _eventBroker = ServiceLocator.Get<IEventBroker>();
            _userState = ServiceLocator.Get<IUserState>();
            _pageNavigator = ServiceLocator.Get<IPageNavigator>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (await _userState.IsAuthenticated())
            {
                await _pageNavigator.NavigateTo<NotesPage>();
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

        private async void btnRegister_OnClick(object sender, EventArgs e)
        {
            await _pageNavigator.NavigateTo<RegisterPage>();
        }
    }
}

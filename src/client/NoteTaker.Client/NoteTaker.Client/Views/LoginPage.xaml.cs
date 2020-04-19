using System;
using System.Threading.Tasks;
using NoteTaker.Client.Events;
using NoteTaker.Client.Events.AuthEvents;
using NoteTaker.Client.Extensions;
using NoteTaker.Client.Navigation;
using NoteTaker.Client.State;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTaker.Client.Views
{
    [AllowAnonymous]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private IEventBroker _eventBroker;

        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _eventBroker = ServiceLocator.Get<IEventBroker>();
            _eventBroker.Listen<UserLoggedInCommand>(userLoggedInCommandHandler);
            _eventBroker.Listen<LoginErrorCommand>(loginErrorCommandHandler);

            if (UserState.IsAuthenticated())
            {
                PageNavigator.NavigateTo<NotesPage>();
            }
            else
            {
                boxLogin.SetDynamicWidth();
                BackgroundImageSource = ImageSource.FromResource("NoteTaker.Client.Assets.loginbg.jpg");
            }
        }

        private Task userLoggedInCommandHandler(UserLoggedInCommand arg)
        {
            PageNavigator.NavigateTo<NotesPage>();
            return Task.CompletedTask;
        }

        private Task loginErrorCommandHandler(LoginErrorCommand arg)
        {
            return DisplayAlert("Invalid user", "User not registered.", "Ok");
        }

        private async void btnLogin_OnClick(object sender, EventArgs e)
        {
            await _eventBroker.Command(new LoginCommand(txtEmail.Text, txtPassword.Text));
        }

        private void btnRegister_OnClick(object sender, EventArgs e)
        {
            PageNavigator.NavigateTo<RegisterPage>();
        }
    }
}

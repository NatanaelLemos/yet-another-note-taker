using System;
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
    public partial class RegisterPage : ContentPage
    {
        private IEventBroker _eventBroker;

        public RegisterPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _eventBroker = ServiceLocator.Get<IEventBroker>();

            boxRegister.SetDynamicWidth();
            BackgroundImageSource = ImageSource.FromResource("NoteTaker.Client.Assets.loginbg.jpg");
        }

        private async void btnRegister_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                await DisplayAlert("Invalid email", "The provided email is not valid.", "Ok");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                await DisplayAlert("Invalid password", "The provided password is not valid.", "Ok");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                await DisplayAlert("Invalid password", "The provided password is not valid.", "Ok");
                return;
            }

            if (txtPassword.Text != txtRepeatPassword.Text)
            {
                await DisplayAlert("Invalid password", "The provided passwords do not match.", "Ok");
                return;
            }

            await _eventBroker.Command(new CreateUserCommand(txtEmail.Text, txtPassword.Text));
        }

        private void btnBackToLogin_OnClick(object sender, EventArgs e)
        {
            PageNavigator.NavigateTo<LoginPage>();
        }
    }
}

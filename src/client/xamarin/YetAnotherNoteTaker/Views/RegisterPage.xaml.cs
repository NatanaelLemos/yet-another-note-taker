using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YetAnotherNoteTaker.Client.Common.Events;
using YetAnotherNoteTaker.Client.Common.Events.AuthEvents;
using YetAnotherNoteTaker.Client.Common.Security;
using YetAnotherNoteTaker.Extensions;
using YetAnotherNoteTaker.Helpers;
using YetAnotherNoteTaker.State;

namespace YetAnotherNoteTaker.Views
{
    [AllowAnonymous]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private IEventBroker _eventBroker;

        public RegisterPage()
        {
            InitializeComponent();
            _eventBroker = ServiceLocator.Get<IEventBroker>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            boxRegister.SetDynamicWidth();
            BackgroundImageSource = FileHelpers.ReadAsImageSource("YetAnotherNoteTaker.Assets.loginbg.jpg");

            txtEmail.Focus();
            txtEmail.Completed += (object sender, EventArgs e) => txtPassword.Focus();
            txtPassword.Completed += (object sender, EventArgs e) => txtRepeatPassword.Focus();
            txtRepeatPassword.Completed += (object sender, EventArgs e) => btnRegister_OnClick(sender, e);
        }

        private async void btnRegister_OnClick(object sender, EventArgs e)
        {
            if (!ValidationHelpers.IsValidEmail(txtEmail.Text))
            {
                await DisplayAlert("Invalid email", "The provided email is not valid.", "Ok");
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

            try
            {
                await _eventBroker.Notify(new CreateUserCommand(txtEmail.Text, txtPassword.Text));
                PageNavigator.NavigateTo<LoginPage>();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Invalid registration", ex.Message, "Ok");
                return;
            }
        }

        private void btnBackToLogin_OnClick(object sender, EventArgs e)
        {
            PageNavigator.NavigateTo<LoginPage>();
        }
    }
}

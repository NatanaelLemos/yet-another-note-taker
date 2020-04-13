using System;
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
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

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

        private void btnLogin_OnClick(object sender, EventArgs e)
        {
        }

        private void btnRegister_OnClick(object sender, EventArgs e)
        {
            PageNavigator.NavigateTo<RegisterPage>();
        }
    }
}

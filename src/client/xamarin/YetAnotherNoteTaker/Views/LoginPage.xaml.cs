using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YetAnotherNoteTaker.Events;
using YetAnotherNoteTaker.Events.AuthEvents;
using YetAnotherNoteTaker.Events.NotebookEvents;
using YetAnotherNoteTaker.Events.NoteEvents;
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
        }

        private async void btnLogin_OnClick(object sender, EventArgs e)
        {
            try
            {
                await _eventBroker.Notify(new LoginCommand(txtEmail.Text, txtPassword.Text));

                PageNavigator.NavigateTo<NotesPage>();
                await _eventBroker.Notify(new ListNotebooksCommand());
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

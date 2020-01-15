using System;
using System.Threading.Tasks;
using NoteTaker.Client.Extensions;
using NoteTaker.Client.State;
using NoteTaker.Client.State.SocketEvents;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTaker.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SyncPage : ContentPage
    {
        private IEventBroker _eventBroker;

        public SyncPage()
        {
            InitializeComponent();
            boxSync.SetDynamicWidth();

            if (Environment.OSVersion.Platform != PlatformID.Unix)
            {
                this.SizeChanged += SyncPage_SizeChanged;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _eventBroker = ServiceLocator.Get<IEventBroker>();

            _eventBroker.Listen<SocketStartedListeningCommand>(c =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    lblStatus.Text = $"Server started on {c.IP} : {c.Port}\n{lblStatus.Text}";
                });
                return Task.CompletedTask;
            });

            _eventBroker.Listen<SocketStoppedListeningCommand>(c =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    lblStatus.Text = $"Server stopped on {c.IP} : {c.Port}\n{lblStatus.Text}";
                });
                return Task.CompletedTask;
            });

            _eventBroker.Listen<ClientConnectedCommand>(c =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {

                });
                return Task.CompletedTask;
            });

            lblStatus.HeightRequest = Application.Current.MainPage.Height - 300;
            lblStatus.Focus();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await _eventBroker.Command(new StopListeningCommand());
        }

        private void SyncPage_SizeChanged(object sender, EventArgs e)
        {
            lblStatus.HeightRequest = Application.Current.MainPage.Height - 300;
        }

        private async void btnStartListening_OnClick(object sender, EventArgs e)
        {
            await _eventBroker.Command(new StartListeningCommand());
        }

        private async void btnSendData_OnClick(object sender, EventArgs e)
        {
            await _eventBroker.Command(new SendDataToSocketCommand(txtServer.Text, Convert.ToInt32(txtPort.Text)));
        }
    }
}

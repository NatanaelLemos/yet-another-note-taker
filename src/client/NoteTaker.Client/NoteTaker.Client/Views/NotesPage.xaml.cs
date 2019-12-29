using NoteTaker.Client.Services;
using NoteTaker.Client.State;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTaker.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        private readonly INotesAppService _service;

        public NotesPage()
        {
            InitializeComponent();
            _service = ServiceLocator.Get<INotesAppService>();
            lsvNotes.ItemsSource = _service.DataSource;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Title = "All notes";
            await _service.FetchAll();
        }
    }
}

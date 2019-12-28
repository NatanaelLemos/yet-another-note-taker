using System;
using NoteTaker.Client.Services;
using NoteTaker.Client.State;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTaker.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotebooksPage : ContentPage
    {
        private readonly INotebooksService _service;

        public NotebooksPage()
        {
            InitializeComponent();

            _service = ServiceLocator.Get<INotebooksService>();
            lsvNotebooks.ItemsSource = _service.DataSource;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _service.FetchAll();
        }

        private void btnNewNotebook_OnClick(object sender, EventArgs e)
        {
            PageNavigator.NavigateTo<NotebookEditorPage>();
        }
    }
}

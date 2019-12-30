using System;
using NoteTaker.Client.Services;
using NoteTaker.Client.State;
using NoteTaker.Domain.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTaker.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotebooksPage : ContentPage
    {
        private readonly INotebooksAppService _service;

        public NotebooksPage()
        {
            InitializeComponent();

            _service = ServiceLocator.Get<INotebooksAppService>();
            lsvNotebooks.ItemsSource = _service.DataSource;
            lsvNotebooks.ItemTapped += LsvNotebooks_ItemTapped;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _service.FetchAll();
        }

        private void LsvNotebooks_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var notebookDto = e?.Item as NotebookDto;
            PageNavigator.NavigateTo<NotesPage>(notebookDto);
        }

        private void btnNewNotebook_OnClick(object sender, EventArgs e)
        {
            lsvNotebooks.SelectedItem = null;
            PageNavigator.NavigateTo<NotebookEditorPage>();
        }

        private void btnAllNotes_OnClick(object sender, EventArgs e)
        {
            lsvNotebooks.SelectedItem = null;
            PageNavigator.NavigateTo<NotesPage>();
        }
    }
}

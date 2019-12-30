using System;
using System.Linq;
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

        private void btnEditNotebook_OnClick(object sender, EventArgs e)
        {
            var notebookId = (sender as Button)?.BindingContext as Guid?;
            if (notebookId == null)
            {
                return;
            }

            PageNavigator.NavigateTo<NotebookEditorPage>(notebookId);
        }

        private async void btnRemoveNotebook_OnClick(object sender, EventArgs e)
        {
            var notebookId = (sender as Button)?.BindingContext as Guid?;
            if (notebookId == null)
            {
                return;
            }

            var answer = await DisplayAlert("Remove", "Are you sure?", "Yes", "No");
            if (answer)
            {
                await _service.Delete(notebookId.Value);
            }
        }
    }
}

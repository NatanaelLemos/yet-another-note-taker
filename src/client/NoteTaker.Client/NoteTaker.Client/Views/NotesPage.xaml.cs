using System;
using NoteTaker.Client.Services;
using NoteTaker.Client.State;
using NoteTaker.Domain;
using NoteTaker.Domain.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTaker.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        private readonly INotesAppService _service;
        private readonly NotebookDto _notebook;

        public NotesPage()
        {
            InitializeComponent();
            _service = ServiceLocator.Get<INotesAppService>();
            lsvNotes.ItemsSource = _service.DataSource;
            lsvNotes.ItemTapped += LsvNotes_ItemTapped;
        }

        public NotesPage(NotebookDto notebook)
            : this()
        {
            _notebook = notebook;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_notebook == null)
            {
                Title = "All notes";
                await _service.FetchAll();

                ToolbarItems.RemoveAt(0);
            }
            else
            {
                Title = _notebook.Name;
                await _service.FilterByNotebookId(_notebook.Id);
            }
        }

        private void btnNewNote_OnClick(object sender, EventArgs e)
        {
            PageNavigator.NavigateTo<NoteEditorPage>(_notebook);
        }

        private void LsvNotes_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            PageNavigator.NavigateTo<NoteEditorPage>(_notebook, e.Item as NoteListItemDto);
        }
    }
}

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
            }
            else
            {
                Title = _notebook.Name;
                await _service.FilterByNotebookId(_notebook.Id);
            }
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Events;
using YetAnotherNoteTaker.Events.NoteEvents;
using YetAnotherNoteTaker.State;

namespace YetAnotherNoteTaker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        private IEventBroker _eventBroker;
        private ObservableCollection<NoteDto> _dataSource;
        private NotebookDto _notebook;

        public NotesPage()
        {
            InitializeComponent();
            Initialize(null);
        }

        public NotesPage(NotebookDto notebook)
        {
            InitializeComponent();
            Initialize(notebook);
        }

        private void Initialize(NotebookDto notebook)
        {
            _dataSource = new ObservableCollection<NoteDto>();
            lsvNotes.ItemsSource = _dataSource;
            lsvNotes.ItemTapped += LsvNotes_ItemTapped;

            _eventBroker = ServiceLocator.Get<IEventBroker>();
            _eventBroker.Subscribe<ListNotesResult>(ListNotesResultHandler);

            if (notebook == null)
            {
                Title = "All notes";
                ToolbarItems.RemoveAt(0);
                _eventBroker.Notify(new ListNotesCommand());
                return;
            }

            Title = notebook?.Name;
            _eventBroker.Notify(new ListNotesCommand(notebook.Key));
            _notebook = notebook;
        }

        private void LsvNotes_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            PageNavigator.NavigateTo<NoteEditorPage>(_notebook, e.Item as NoteDto);
        }

        private Task ListNotesResultHandler(ListNotesResult arg)
        {
            _dataSource.Clear();
            foreach (var item in arg.Notes)
            {
                _dataSource.Add(item);
            }

            return Task.CompletedTask;
        }

        private void btnNewNote_OnClick(object sender, EventArgs e)
        {
            if (_notebook == null)
            {
                return;
            }

            PageNavigator.NavigateTo<NoteEditorPage>(_notebook);
        }
    }
}

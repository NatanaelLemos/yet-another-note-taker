using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NLemos.Xamarin.Common.State;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YetAnotherNoteTaker.Blazor.State;
using YetAnotherNoteTaker.Client.Common.Events;
using YetAnotherNoteTaker.Client.Common.Events.NoteEvents;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        private IEventBroker _eventBroker;
        private IPageNavigator _pageNavigator;
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

            _pageNavigator = ServiceLocator.Get<IPageNavigator>();

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

        private async void LsvNotes_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await _pageNavigator.NavigateTo<NoteEditorPage>(_notebook, e.Item as NoteDto);
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

        private async void btnNewNote_OnClick(object sender, EventArgs e)
        {
            if (_notebook == null)
            {
                return;
            }

            await _pageNavigator.NavigateTo<NoteEditorPage>(_notebook);
        }
    }
}

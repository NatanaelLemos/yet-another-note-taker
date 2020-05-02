using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YetAnotherNoteTaker.Client.Common.Dtos;
using YetAnotherNoteTaker.Events;
using YetAnotherNoteTaker.Events.NoteEvents;
using YetAnotherNoteTaker.Events.SettingsEvents;
using YetAnotherNoteTaker.State;

namespace YetAnotherNoteTaker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteEditorPage : ContentPage
    {
        private readonly IEventBroker _eventBroker;
        private readonly NotebookDto _notebook;
        private readonly NoteDto _note;

        private QuillEditor _textEditor;

        public NoteEditorPage(NotebookDto notebook)
        {
            InitializeComponent();
            _eventBroker = ServiceLocator.Get<IEventBroker>();

            Title = "New note";
            ToolbarItems.RemoveAt(0);

            _notebook = notebook;

            InitializeEditor("");
        }

        public NoteEditorPage(NotebookDto notebook, NoteDto note)
        {
            InitializeComponent();
            _eventBroker = ServiceLocator.Get<IEventBroker>();

            Title = note.Name;
            txtName.Text = note.Name;

            _notebook = notebook;
            _note = note;

            InitializeEditor(note.Body);
        }

        private void InitializeEditor(string body)
        {
            _eventBroker.Subscribe<SettingsQueryResult>(arg =>
            {
                var height = Application.Current.MainPage.Height - 130;
                _textEditor = new QuillEditor(webEditor, height, body, arg.Settings.IsDarkMode);
                return Task.CompletedTask;
            });

            _eventBroker.Notify(new SettingsQuery());
        }

        private async void btnRemoveNote_OnClick(object sender, EventArgs e)
        {
            await _eventBroker.Notify(new DeleteNoteCommand(_notebook.Id, _note.Id));
            PageNavigator.NavigateTo<NotesPage>(_notebook);
        }

        private async void btnSave_OnClick(object sender, EventArgs e)
        {
            var body = await _textEditor.GetContent();
            await _eventBroker.Notify(
                new EditNoteCommand(
                    _notebook?.Id ?? Guid.Empty,
                    _note?.Id ?? Guid.Empty,
                    txtName.Text,
                    body));

            if (_notebook == null)
            {
                PageNavigator.NavigateTo<NotesPage>();
            }
            else
            {
                PageNavigator.NavigateTo<NotesPage>(_notebook);
            }
        }

        private void btnCancel_OnClick(object sender, EventArgs e)
        {
            PageNavigator.Back();
        }
    }
}

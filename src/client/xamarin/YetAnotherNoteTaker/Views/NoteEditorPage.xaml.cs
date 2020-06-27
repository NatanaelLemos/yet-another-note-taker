using System;
using System.Threading.Tasks;
using NLemos.Xamarin.Common.State;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YetAnotherNoteTaker.Blazor.State;
using YetAnotherNoteTaker.Client.Common.Events;
using YetAnotherNoteTaker.Client.Common.Events.NoteEvents;
using YetAnotherNoteTaker.Client.Common.Events.SettingsEvents;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteEditorPage : ContentPage
    {
        private readonly NoteDto _note;

        private QuillEditor _textEditor;

        private IEventBroker _eventBroker;
        private IPageNavigator _pageNavigator;
        private NotebookDto _notebook;

        public NoteEditorPage(NotebookDto notebook)
        {
            InitializeComponent();
            Initialize(notebook);

            Title = "New note";
            ToolbarItems.RemoveAt(0);


            InitializeEditor("");
            SizeChanged += NoteEditorPage_SizeChanged;
        }

        public NoteEditorPage(NotebookDto notebook, NoteDto note)
        {
            InitializeComponent();
            Initialize(notebook);

            Title = note.Name;
            txtName.Text = note.Name;

            _note = note;

            InitializeEditor(note.Body);
        }

        private void Initialize(NotebookDto notebook)
        {
            _eventBroker = ServiceLocator.Get<IEventBroker>();
            _pageNavigator = ServiceLocator.Get<IPageNavigator>();
            _notebook = notebook;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _eventBroker.Notify(new SettingsQuery());
            txtName.Completed += (object sender, EventArgs e) => _textEditor.Focus();
        }

        private void InitializeEditor(string body)
        {
            _eventBroker.Subscribe<SettingsQueryResult>(arg =>
            {
                var height = Application.Current.MainPage.Height - 130;
                _textEditor = new QuillEditor(webEditor, height, body, arg.Settings.IsDarkMode);

                if (_note == null)
                {
                    txtName.Focus();
                }
                else
                {
                    _textEditor.Focus();
                }

                return Task.CompletedTask;
            });
        }

        private void NoteEditorPage_SizeChanged(object sender, EventArgs e)
        {
            if (_textEditor != null)
            {
                _textEditor.Height = Application.Current.MainPage.Height - 130;
            }
        }

        private async void btnRemoveNote_OnClick(object sender, EventArgs e)
        {
            await _eventBroker.Notify(new DeleteNoteCommand(GetNotebookKey(), _note.Key));
            await _pageNavigator.NavigateTo<NotesPage>(_notebook);
        }

        private async void btnSave_OnClick(object sender, EventArgs e)
        {
            var body = await _textEditor.GetContent();
            await _eventBroker.Notify(
                new EditNoteCommand(
                    GetNotebookKey(),
                    _note?.Key ?? string.Empty,
                    txtName.Text,
                    body));

            if (_notebook == null)
            {
                await _pageNavigator.NavigateTo<NotesPage>();
            }
            else
            {
                await _pageNavigator.NavigateTo<NotesPage>(_notebook);
            }
        }

        private string GetNotebookKey()
        {
            if (!string.IsNullOrWhiteSpace(_notebook?.Key))
            {
                return _notebook.Key;
            }

            if (!string.IsNullOrWhiteSpace(_note?.NotebookKey))
            {
                return _note.NotebookKey;
            }

            return string.Empty;
        }

        private async void btnCancel_OnClick(object sender, EventArgs e)
        {
            await _pageNavigator.Back();
        }
    }
}

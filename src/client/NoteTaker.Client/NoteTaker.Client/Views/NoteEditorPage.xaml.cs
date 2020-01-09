using System;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using NoteTaker.Client.Extensions;
using NoteTaker.Client.State;
using NoteTaker.Client.State.NoteEvents;
using NoteTaker.Client.State.SettingsEvents;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTaker.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteEditorPage : ContentPage
    {
        private IEventBroker _eventBroker;
        private NotebookDto _notebook;
        private NoteDto _dto;
        private QuillEditor _textEditor;
        private Timer _updateTimer;

        public NoteEditorPage()
        {
            InitializeComponent();
            boxName.SetDynamicWidth();

            _updateTimer = new Timer(1000);
            _updateTimer.Elapsed += UpdateTimer_Elapsed;

            SizeChanged += NoteEditorPage_SizeChanged;
        }

        public NoteEditorPage(NotebookDto notebook)
            : this()
        {
            _notebook = notebook;
            _dto = new NoteDto
            {
                NotebookId = notebook.Id
            };
        }

        public NoteEditorPage(NotebookDto notebook, NoteDto dto)
            : this()
        {
            _notebook = notebook;
            _dto = dto;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _eventBroker = ServiceLocator.Get<IEventBroker>();

            txtName.Text = _dto.Name;
            SetTitle();

            if (_dto.Id == Guid.Empty)
            {
                txtName.Focus();
            }
            else
            {
                webEditor.Focus();
            }

            var height = Application.Current.MainPage.Height;

            var settings = await _eventBroker.Query<SettingsQuery, Settings>(new SettingsQuery());
            _textEditor = new QuillEditor(webEditor, "NoteEditor", height - 130, _dto.Text, settings.DarkMode);
            _updateTimer.Start();
        }

        protected override void OnDisappearing()
        {
            _updateTimer.Stop();
            base.OnDisappearing();
        }

        private void NoteEditorPage_SizeChanged(object sender, EventArgs e)
        {
            if (_textEditor != null)
            {
                _textEditor.Height = Application.Current.MainPage.Height - 130;
            }
        }

        private void SetTitle()
        {
            var title = new StringBuilder();
            if (_notebook == null)
            {
                if (!string.IsNullOrEmpty(_dto?.NotebookName))
                {
                    title.Append(_dto.NotebookName);
                    title.Append(" / ");
                }
            }
            else
            {
                title.Append(_notebook.Name);
                title.Append(" / ");
            }

            if (_dto.Id == Guid.Empty)
            {
                title.Append("New note");
            }
            else
            {
                title.Append(_dto.Name);
            }

            Title = title.ToString();
        }

        private async void btnRemoveNote_OnClick(object sender, EventArgs e)
        {
            if (_dto.Id == Guid.Empty)
            {
                return;
            }

            var answer = await DisplayAlert("Remove", "Are you sure?", "Yes", "No");
            if (answer)
            {
                await _eventBroker.Command(new DeleteNoteCommand(_dto));
                PageNavigator.Back();
            }
        }

        private void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _updateTimer.Stop();

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var quill = await _textEditor.GetContent();
                    await UpdateDto(txtName.Text, quill);
                }
                finally
                {
                    _updateTimer.Start();
                }
            });
        }

        private Task UpdateDto(string name, string text)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Task.CompletedTask;
            }

            if (_dto.Name == name && _dto.Text == text)
            {
                return Task.CompletedTask;
            }

            _dto.Name = name;
            _dto.Text = text;

            if (_dto.Id == Guid.Empty)
            {
                _dto.Id = Guid.NewGuid();
                return _eventBroker.Command(new CreateNoteCommand(_dto));
            }
            else
            {
                return _eventBroker.Command(new UpdateNoteCommand(_dto));
            }
        }
    }
}

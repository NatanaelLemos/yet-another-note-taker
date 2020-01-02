using System;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Client.Extensions;
using NoteTaker.Client.Services;
using NoteTaker.Client.State;
using NoteTaker.Client.State.NoteEvents;
using NoteTaker.Domain.Dtos;
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

        public NoteEditorPage()
        {
            InitializeComponent();
            boxNote.SetDynamicWidth();

            txtName.TextChanged += TxtName_TextChanged;
            txtText.TextChanged += TxtText_TextChanged;

            txtText.Focused += TxtText_Focused;
        }

        private void TxtText_Focused(object sender, FocusEventArgs e)
        {
            this.Title += "a";
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _eventBroker = ServiceLocator.Get<IEventBroker>();

            txtName.Text = _dto.Name;
            txtText.Text = _dto.Text;

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
                txtName.Focus();
            }
            else
            {
                title.Append(_dto.Name);
                txtText.Focus();
            }

            Title = title.ToString();
        }

        private async void TxtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                return;
            }

            _dto.Name = e.NewTextValue;
            await UpdateDto();
        }

        private async void TxtText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                return;
            }

            _dto.Text = e.NewTextValue;
            await UpdateDto();
        }

        private Task UpdateDto()
        {
            if (_dto.Id == Guid.Empty)
            {
                if (string.IsNullOrEmpty(_dto.Name))
                {
                    return Task.CompletedTask;
                }

                _dto.Id = Guid.NewGuid();
                return _eventBroker.Command(new CreateNoteCommand(_dto));
            }
            else
            {
                return _eventBroker.Command(new UpdateNoteCommand(_dto));
            }
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

        private void btnCheckboxUnchecked_OnClick(object sender, EventArgs e)
        {
            txtText.Text += "☐";
            txtText.Focus();
        }

        private void btnCheckboxChecked_OnClick(object sender, EventArgs e)
        {
            txtText.Text += "☒";
            txtText.Focus();
        }
    }
}

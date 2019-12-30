using System.Text;
using NoteTaker.Client.Extensions;
using NoteTaker.Client.Services;
using NoteTaker.Client.State;
using NoteTaker.Domain.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTaker.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteEditorPage : ContentPage
    {
        private readonly NotebookDto _notebook;
        private readonly NoteListItemDto _noteListItem;

        private readonly INotesAppService _service;

        public NoteEditorPage()
        {
            InitializeComponent();
            boxNote.SetDynamicWidth();

            _service = ServiceLocator.Get<INotesAppService>();
            _service.Current.Bind(nameof(NoteDetailDto.Name), txtName);
            _service.Current.Bind(nameof(NoteDetailDto.Text), txtText);
        }

        public NoteEditorPage(NotebookDto notebook)
            : this()
        {
            _notebook = notebook;
        }

        public NoteEditorPage(NotebookDto notebook, NoteListItemDto noteListItem)
            : this()
        {
            _notebook = notebook;
            _noteListItem = noteListItem;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var title = new StringBuilder();

            if (_notebook != null)
            {
                title.Append(_notebook.Name);
                title.Append(" / ");
            }

            if (_noteListItem == null)
            {
                title.Append("New note");
                _service.NewNote(_notebook.Id);
            }
            else
            {
                title.Append(_noteListItem.Name);
                await _service.LoadNote(_noteListItem.Id);
                txtText.Focus();
            }

            Title = title.ToString();
        }
    }
}

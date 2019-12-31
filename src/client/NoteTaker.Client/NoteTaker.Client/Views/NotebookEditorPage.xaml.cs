using System;
using NoteTaker.Client.Extensions;
using NoteTaker.Client.Services;
using NoteTaker.Client.State;
using NoteTaker.Client.State.NotebookEvents;
using NoteTaker.Client.State.NoteEvents;
using NoteTaker.Domain.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTaker.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotebookEditorPage : ContentPage
    {
        private readonly IEventBroker _eventBroker;
        private readonly Guid? _id;

        private NotebookDto _dto;

        public NotebookEditorPage()
        {
            InitializeComponent();
            boxNotebook.SetDynamicWidth();

            _eventBroker = ServiceLocator.Get<IEventBroker>();

            txtNotebook.TextChanged += TxtNotebook_TextChanged;
        }

        public NotebookEditorPage(Guid id)
            : this()
        {
            _id = id;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_id == null || _id == Guid.Empty)
            {
                Title = "New notebook";
                _dto = new NotebookDto();
            }
            else
            {
                _dto = await _eventBroker.Query<NotebookQuery, NotebookDto>(new NotebookQuery
                {
                    NotebookId = _id.Value
                });

                Title = _dto.Name;
                txtNotebook.Text = _dto.Name;
            }
        }

        private async void TxtNotebook_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                return;
            }

            _dto.Name = e.NewTextValue;

            if (_dto.Id == Guid.Empty)
            {
                _dto.Id = Guid.NewGuid();
                await _eventBroker.Command(new CreateNotebookCommand(_dto));
            }
            else
            {
                await _eventBroker.Command(new UpdateNotebookCommand(_dto));
            }
        }
    }
}

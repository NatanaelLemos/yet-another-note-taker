using System;
using NoteTaker.Client.Events;
using NoteTaker.Client.Events.NotebookEvents;
using NoteTaker.Client.Extensions;
using NoteTaker.Client.State;
using NoteTaker.Domain.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTaker.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotebookEditorPage : ContentPage
    {
        private readonly Guid? _id;

        private IEventBroker _eventBroker;
        private NotebookDto _dto;

        public NotebookEditorPage()
        {
            InitializeComponent();
            boxNotebook.SetDynamicWidth();
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

            _eventBroker = ServiceLocator.Get<IEventBroker>();

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

            txtNotebook.Focus();
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

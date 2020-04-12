using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using NoteTaker.Client.Events;
using NoteTaker.Client.Events.NoteEvents;
using NoteTaker.Client.Navigation;
using NoteTaker.Client.State;
using NoteTaker.Domain.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTaker.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        private readonly NotebookDto _notebook;

        private IEventBroker _eventBroker;
        private ObservableCollection<NoteDto> _dataSource;

        public NotesPage()
        {
            InitializeComponent();

            _dataSource = new ObservableCollection<NoteDto>();
            lsvNotes.ItemsSource = _dataSource;
            lsvNotes.ItemTapped += LsvNotes_ItemTapped;
        }

        public NotesPage(NotebookDto notebook)
            : this()
        {
            _notebook = notebook;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            _eventBroker = ServiceLocator.Get<IEventBroker>();

            _eventBroker.Listen<CreateNoteCommand>(c =>
            {
                _dataSource.Insert(0, c.Dto);
                return Task.CompletedTask;
            });

            _eventBroker.Listen<UpdateNoteCommand>(c =>
            {
                RemoveItemFromDataSource(c.Dto);
                _dataSource.Insert(0, c.Dto);
                return Task.CompletedTask;
            });

            _eventBroker.Listen<DeleteNoteCommand>(c =>
            {
                RemoveItemFromDataSource(c.Dto);
                return Task.CompletedTask;
            });

            var query = new NoteQuery();

            if (_notebook == null)
            {
                Title = "All notes";
                if (ToolbarItems.Count() > 0)
                {
                    ToolbarItems.RemoveAt(0);
                }
            }
            else
            {
                query.NotebookId = _notebook.Id;
                Title = _notebook.Name;
            }

            _dataSource.Clear();
            var data = await _eventBroker.Query<NoteQuery, ICollection<NoteDto>>(query);

            foreach (var item in data)
            {
                _dataSource.Add(item);
            }
        }

        private void RemoveItemFromDataSource(NoteDto dto)
        {
            var itemToRemove = _dataSource.FirstOrDefault(d => d.Id == dto.Id);

            if (itemToRemove == null)
            {
                return;
            }

            _dataSource.Remove(itemToRemove);
        }

        private void btnNewNote_OnClick(object sender, EventArgs e)
        {
            PageNavigator.NavigateTo<NoteEditorPage>(_notebook);
        }

        private void LsvNotes_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            PageNavigator.NavigateTo<NoteEditorPage>(_notebook, e.Item as NoteDto);
        }
    }
}

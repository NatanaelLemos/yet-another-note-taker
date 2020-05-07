using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Events;
using YetAnotherNoteTaker.Events.NotebookEvents;
using YetAnotherNoteTaker.State;

namespace YetAnotherNoteTaker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotebooksSidebarPage : ContentPage
    {
        private readonly ObservableCollection<NotebookDto> _dataSource;
        private IEventBroker _eventBroker;

        public NotebooksSidebarPage()
        {
            InitializeComponent();

            _dataSource = new ObservableCollection<NotebookDto>();
            lsvNotebooks.ItemsSource = _dataSource;
            lsvNotebooks.ItemTapped += LsvNotebooks_ItemTapped;

            _eventBroker = ServiceLocator.Get<IEventBroker>();
            _eventBroker.Subscribe<ListNotebooksResult>(ListNotebooksResultHandler);

            btnAllNotes.IsVisible = false;
            gridActions.IsVisible = false;
        }

        private Task ListNotebooksResultHandler(ListNotebooksResult arg)
        {
            _dataSource.Clear();
            foreach (var item in arg.Notebooks)
            {
                _dataSource.Add(item);
            }

            btnAllNotes.IsVisible = true;
            gridActions.IsVisible = true;

            return Task.CompletedTask;
        }

        private void LsvNotebooks_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var notebookDto = e?.Item as NotebookDto;
            PageNavigator.NavigateTo<NotesPage>(notebookDto);
        }

        private void btnAllNotes_OnClick(object sender, EventArgs e)
        {
            PageNavigator.NavigateTo<NotesPage>();
        }

        private void btnEditNotebook_OnClick(object sender, EventArgs e)
        {
            var notebookId = (sender as Button)?.BindingContext as Guid?;
            if (notebookId == null)
            {
                return;
            }

            var notebook = _dataSource.FirstOrDefault(n => n.Id == notebookId);
            PageNavigator.NavigateTo<NotebookEditorPage>(notebook);
        }

        private async void btnRemoveNotebook_OnClick(object sender, EventArgs e)
        {
            var notebookId = (sender as Button)?.BindingContext as Guid?;
            if (notebookId == null)
            {
                return;
            }

            var answer = await DisplayAlert("Remove", "Are you sure?", "Yes", "No");
            if (answer)
            {
                await _eventBroker.Notify(new DeleteNotebookCommand(notebookId.Value));
            }
        }

        private void btnNewNotebook_OnClick(object sender, EventArgs e)
        {
            PageNavigator.NavigateTo<NotebookEditorPage>();
        }

        private void btnSettings_OnClick(object sender, EventArgs e)
        {
            PageNavigator.NavigateTo<SettingsPage>();
        }
    }
}

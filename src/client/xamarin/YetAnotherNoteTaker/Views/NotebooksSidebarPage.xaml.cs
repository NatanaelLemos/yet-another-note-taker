using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using NLemos.Xamarin.Common.State;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YetAnotherNoteTaker.Blazor.State;
using YetAnotherNoteTaker.Client.Common.Events;
using YetAnotherNoteTaker.Client.Common.Events.NotebookEvents;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.State;
using Observatron;

namespace YetAnotherNoteTaker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotebooksSidebarPage : ContentPage
    {
        private readonly ObservableCollection<NotebookDto> _dataSource;
        private readonly IEventBroker _eventBroker;
        private readonly IPageNavigator _pageNavigator;

        public NotebooksSidebarPage()
        {
            InitializeComponent();

            _dataSource = new ObservableCollection<NotebookDto>();
            lsvNotebooks.ItemsSource = _dataSource;
            lsvNotebooks.ItemTapped += LsvNotebooks_ItemTapped;

            _eventBroker = ServiceLocator.Get<IEventBroker>();
            _eventBroker.Subscribe<ListNotebooksResult>(ListNotebooksResultHandler);

            _pageNavigator = ServiceLocator.Get<IPageNavigator>();

            btnAllNotes.IsVisible = false;
            gridActions.IsVisible = false;
        }

        private Task ListNotebooksResultHandler(ListNotebooksResult arg)
        {
            _dataSource.Clear();
            foreach (var item in arg?.Notebooks ?? new List<NotebookDto>())
            {
                _dataSource.Add(item);
            }

            btnAllNotes.IsVisible = true;
            gridActions.IsVisible = true;

            return Task.CompletedTask;
        }

        private async void LsvNotebooks_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var notebookDto = e?.Item as NotebookDto;
            await _pageNavigator.NavigateTo<NotesPage>(notebookDto);
        }

        private async void btnAllNotes_OnClick(object sender, EventArgs e)
        {
            await _pageNavigator.NavigateTo<NotesPage>();
        }

        private async void btnEditNotebook_OnClick(object sender, EventArgs e)
        {
            var notebookKey = GetNotebookKey(sender);
            if (string.IsNullOrWhiteSpace(notebookKey))
            {
                return;
            }

            var notebook = _dataSource.FirstOrDefault(n => n.Key == notebookKey);
            await _pageNavigator.NavigateTo<NotebookEditorPage>(notebook);
        }

        private string GetNotebookKey(object sender)
        {
            return (sender as Button)?.BindingContext as string;
        }

        private async void btnRemoveNotebook_OnClick(object sender, EventArgs e)
        {
            var notebookKey = GetNotebookKey(sender);
            if (notebookKey == null)
            {
                return;
            }

            var answer = await DisplayAlert("Remove", "Are you sure?", "Yes", "No");
            if (answer)
            {
                await _eventBroker.Notify(new DeleteNotebookCommand(notebookKey));
                await _pageNavigator.NavigateTo<NotesPage>();
            }
        }

        private async void btnNewNotebook_OnClick(object sender, EventArgs e)
        {
            await _pageNavigator.NavigateTo<NotebookEditorPage>();
        }

        private async void btnSettings_OnClick(object sender, EventArgs e)
        {
            await _pageNavigator.NavigateTo<SettingsPage>();
        }
    }
}

using System;
using System.Threading.Tasks;
using NLemos.Xamarin.Common.Extensions;
using NLemos.Xamarin.Common.State;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YetAnotherNoteTaker.Blazor.State;
using YetAnotherNoteTaker.Client.Common.Events;
using YetAnotherNoteTaker.Client.Common.Events.NotebookEvents;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotebookEditorPage : ContentPage
    {
        private readonly string _notebookKey;

        private IEventBroker _eventBroker;
        private IPageNavigator _pageNavigator;

        public NotebookEditorPage()
        {
            InitializeComponent();
            Initialize();
            Title = "New notebook";
            _notebookKey = string.Empty;
        }

        public NotebookEditorPage(NotebookDto notebook)
        {
            InitializeComponent();
            Initialize();
            Title = notebook.Name;
            txtNotebook.Text = notebook.Name;
            _notebookKey = notebook.Key;
        }

        private void Initialize()
        {
            boxNotebook.SetDynamicWidth();
            _eventBroker = ServiceLocator.Get<IEventBroker>();
            _eventBroker.Subscribe<EditNotebookResult>(EditNotebookResultHandler);

            _pageNavigator = ServiceLocator.Get<IPageNavigator>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtNotebook.Focus();
            txtNotebook.Completed += (object sender, EventArgs e) => btnSave_Click(sender, e);
        }

        private async Task EditNotebookResultHandler(EditNotebookResult arg)
        {
            await _eventBroker.Notify(new ListNotebooksCommand());
            await _pageNavigator.NavigateTo<NotesPage>(arg.Notebook);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _eventBroker.Notify(new EditNotebookCommand(_notebookKey, txtNotebook.Text));
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            await _pageNavigator.Back();
        }
    }
}

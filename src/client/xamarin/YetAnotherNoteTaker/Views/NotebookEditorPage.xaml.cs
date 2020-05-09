using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Events;
using YetAnotherNoteTaker.Events.NotebookEvents;
using YetAnotherNoteTaker.Extensions;
using YetAnotherNoteTaker.State;

namespace YetAnotherNoteTaker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotebookEditorPage : ContentPage
    {
        private IEventBroker _eventBroker;
        private readonly string _notebookKey;

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
            _eventBroker.Subscribe<EditNotebookResult>(NewNotebookResultHandler);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtNotebook.Focus();
            txtNotebook.Completed += (object sender, EventArgs e) => btnSave_Click(sender, e);
        }

        private async Task NewNotebookResultHandler(EditNotebookResult arg)
        {
            await _eventBroker.Notify(new ListNotebooksCommand());
            PageNavigator.NavigateTo<NotesPage>(arg.Notebook);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _eventBroker.Notify(new EditNotebookCommand(_notebookKey, txtNotebook.Text));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            PageNavigator.Back();
        }
    }
}

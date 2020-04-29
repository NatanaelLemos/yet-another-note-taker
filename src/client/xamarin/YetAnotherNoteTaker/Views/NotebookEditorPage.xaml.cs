using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YetAnotherNoteTaker.Client.Common.Dtos;
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
        private Guid _id;

        public NotebookEditorPage()
        {
            InitializeComponent();
            Initialize();
            Title = "New notebook";
            _id = Guid.Empty;
        }

        public NotebookEditorPage(NotebookDto notebook)
        {
            InitializeComponent();
            Initialize();
            Title = notebook.Name;
            txtNotebook.Text = notebook.Name;
            _id = notebook.Id;
        }

        private void Initialize()
        {
            boxNotebook.SetDynamicWidth();
            _eventBroker = ServiceLocator.Get<IEventBroker>();
            _eventBroker.Subscribe<EditNotebookResult>(NewNotebookResultHandler);
        }

        private async Task NewNotebookResultHandler(EditNotebookResult arg)
        {
            await _eventBroker.Notify(new ListNotebooksCommand());
            PageNavigator.NavigateTo<NotesPage>(arg.Notebook);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _eventBroker.Notify(new EditNotebookCommand(_id, txtNotebook.Text));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            PageNavigator.Back();
        }
    }
}

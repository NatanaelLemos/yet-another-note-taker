using YetAnotherNoteTaker.Client.Common.Dtos;

namespace YetAnotherNoteTaker.Events.NotebookEvents
{
    public class EditNotebookResult
    {
        public EditNotebookResult(NotebookDto notebook)
        {
            Notebook = notebook;
        }

        public NotebookDto Notebook { get; }
    }
}

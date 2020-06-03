namespace YetAnotherNoteTaker.Client.Common.Events.NoteEvents
{
    public class ListNotesCommand
    {
        public ListNotesCommand(string notebookKey = "")
        {
            NotebookKey = notebookKey;
        }

        public string NotebookKey { get; }
    }
}

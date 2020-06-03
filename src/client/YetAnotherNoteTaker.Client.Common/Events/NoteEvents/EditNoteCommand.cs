namespace YetAnotherNoteTaker.Client.Common.Events.NoteEvents
{
    public class EditNoteCommand
    {
        public EditNoteCommand(string notebookKey, string name, string body)
        {
            NotebookKey = notebookKey;
            NoteKey = string.Empty;
            Name = name;
            Body = body;
        }

        public EditNoteCommand(string notebookKey, string noteKey, string name, string body)
        {
            NotebookKey = notebookKey;
            NoteKey = noteKey;
            Name = name;
            Body = body;
        }

        public string NotebookKey { get; }
        public string NoteKey { get; }
        public string Name { get; }
        public string Body { get; }
    }
}

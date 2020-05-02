using System;

namespace YetAnotherNoteTaker.Events.NoteEvents
{
    public class ListNotesCommand
    {
        public ListNotesCommand()
        {
        }

        public ListNotesCommand(Guid notebookId)
        {
            NotebookId = notebookId;
        }

        public Guid NotebookId { get; }
    }
}

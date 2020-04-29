using System;
using System.Collections.Generic;
using System.Text;

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

using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherNoteTaker.Events.NoteEvents
{
    public class DeleteNoteCommand
    {
        public DeleteNoteCommand(Guid notebookId, Guid noteId)
        {
            NotebookId = notebookId;
            NoteId = NoteId;
        }

        public Guid NotebookId { get; }
        public Guid NoteId { get; }
    }
}

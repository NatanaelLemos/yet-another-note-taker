using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherNoteTaker.Events.NoteEvents
{
    public class EditNoteCommand
    {
        public EditNoteCommand(Guid notebookId, string name, string body)
        {
            NotebookId = notebookId;
            NoteId = Guid.Empty;
            Name = name;
            Body = body;
        }

        public EditNoteCommand(Guid notebookId, Guid noteId, string name, string body)
        {
            NotebookId = notebookId;
            NoteId = noteId;
            Name = name;
            Body = body;
        }

        public Guid NotebookId { get; }
        public Guid NoteId { get; }
        public string Name { get; }
        public string Body { get; }
    }
}

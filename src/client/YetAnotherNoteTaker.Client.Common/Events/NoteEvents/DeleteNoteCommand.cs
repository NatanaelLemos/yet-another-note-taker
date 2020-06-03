using System;

namespace YetAnotherNoteTaker.Client.Common.Events.NoteEvents
{
    public class DeleteNoteCommand
    {
        public DeleteNoteCommand(string notebookKey, string noteKey)
        {
            NotebookKey = notebookKey;
            NoteKey = noteKey;
        }

        public string NotebookKey { get; }
        public string NoteKey { get; }
    }
}

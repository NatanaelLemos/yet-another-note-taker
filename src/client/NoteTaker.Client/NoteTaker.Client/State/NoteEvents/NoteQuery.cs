using System;

namespace NoteTaker.Client.State.NoteEvents
{
    public class NoteQuery
    {
        public bool GetAll => NotebookId == Guid.Empty;

        public Guid NotebookId { get; set; } = Guid.Empty;
    }
}

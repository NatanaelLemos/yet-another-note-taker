using System;

namespace NoteTaker.Client.Events.NoteEvents
{
    public class NoteQuery
    {
        public bool GetAll => NotebookId == Guid.Empty;

        public Guid NotebookId { get; set; } = Guid.Empty;
    }
}

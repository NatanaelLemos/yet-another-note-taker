using System;
using System.Collections.Generic;
using System.Text;

namespace NoteTaker.Client.State.NoteEvents
{
    public class NoteQuery
    {
        public bool GetAll => NotebookId == Guid.Empty;

        public Guid NotebookId { get; set; } = Guid.Empty;
    }
}

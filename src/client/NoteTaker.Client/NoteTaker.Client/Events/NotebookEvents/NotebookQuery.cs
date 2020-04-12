using System;

namespace NoteTaker.Client.Events.NotebookEvents
{
    public class NotebookQuery
    {
        public bool GetAll => NotebookId == Guid.Empty;

        public Guid NotebookId { get; set; } = Guid.Empty;
    }
}

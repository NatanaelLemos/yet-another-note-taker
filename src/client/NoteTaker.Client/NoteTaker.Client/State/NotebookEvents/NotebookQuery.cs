using System;
using System.Collections.Generic;
using System.Text;

namespace NoteTaker.Client.State.NotebookEvents
{
    public class NotebookQuery
    {
        public bool GetAll => NotebookId == Guid.Empty;

        public Guid NotebookId { get; set; } = Guid.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherNoteTaker.Events.NotebookEvents
{
    public class DeleteNotebookCommand
    {
        public DeleteNotebookCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}

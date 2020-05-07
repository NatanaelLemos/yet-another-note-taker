using System;

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

using System;

namespace YetAnotherNoteTaker.Events.NotebookEvents
{
    public class EditNotebookCommand
    {
        public EditNotebookCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }
    }
}

using System;

namespace YetAnotherNoteTaker.Events.NotebookEvents
{
    public class DeleteNotebookCommand
    {
        public DeleteNotebookCommand(string key)
        {
            Key = key;
        }

        public string Key { get; }
    }
}

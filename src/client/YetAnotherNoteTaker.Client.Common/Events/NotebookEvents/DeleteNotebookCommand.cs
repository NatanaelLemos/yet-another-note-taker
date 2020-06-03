using System;

namespace YetAnotherNoteTaker.Client.Common.Events.NotebookEvents
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

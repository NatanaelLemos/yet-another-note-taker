using System;

namespace YetAnotherNoteTaker.Events.NotebookEvents
{
    public class EditNotebookCommand
    {
        public EditNotebookCommand(string key, string name)
        {
            Key = key;
            Name = name;
        }

        public string Key { get; }

        public string Name { get; }
    }
}

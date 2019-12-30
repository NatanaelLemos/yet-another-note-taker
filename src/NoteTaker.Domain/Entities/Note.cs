using System;

namespace NoteTaker.Domain.Entities
{
    /// <summary>
    /// Holds the state of a note.
    /// </summary>
    public class Note : EntityBase
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public Guid NotebookId { get; set; }

        public virtual Notebook Notebook { get; set; }
    }
}

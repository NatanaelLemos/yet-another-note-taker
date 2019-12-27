using System;
using System.Collections.Generic;

namespace NoteTaker.Domain
{
    /// <summary>
    /// Collection of notes.
    /// </summary>
    public class Notebook
    {
        public Notebook()
        {
            Id = Guid.NewGuid();
            Notes = new List<Note>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}

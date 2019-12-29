using System;
using System.Collections.Generic;

namespace NoteTaker.Domain.Entities
{
    /// <summary>
    /// Collection of notes.
    /// </summary>
    public class Notebook : EntityBase
    {
        public Notebook()
        {
            Notes = new List<Note>();
        }

        public string Name { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}

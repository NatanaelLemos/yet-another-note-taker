using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherNoteTaker.Client.Common.Dtos
{
    public class NoteDto
    {
        public Guid Id { get; set; }
        public Guid NotebookId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
    }
}

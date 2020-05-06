using System;

namespace YetAnotherNoteTaker.Common.Dtos
{
    public class NoteDto
    {
        public Guid Id { get; set; }
        public Guid NotebookId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
    }
}

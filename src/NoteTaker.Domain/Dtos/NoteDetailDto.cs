using System;

namespace NoteTaker.Domain.Dtos
{
    public class NoteDetailDto
    {
        public Guid Id { get; set; }

        public Guid NotebookId { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }
    }
}

using System;
using NoteTaker.Domain.Dtos;

namespace NoteTaker.Client.Events.NotebookEvents
{
    public class CreateNotebookCommand
    {
        public CreateNotebookCommand(NotebookDto dto)
        {
            if (dto.Id == Guid.Empty)
            {
                dto.Id = Guid.NewGuid();
            }
            Dto = dto;
        }

        public NotebookDto Dto { get; }
    }
}

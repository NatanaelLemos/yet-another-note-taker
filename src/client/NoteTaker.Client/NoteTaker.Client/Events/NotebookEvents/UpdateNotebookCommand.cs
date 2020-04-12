using NoteTaker.Domain.Dtos;

namespace NoteTaker.Client.Events.NotebookEvents
{
    public class UpdateNotebookCommand
    {
        public UpdateNotebookCommand(NotebookDto dto)
        {
            Dto = dto;
        }

        public NotebookDto Dto { get; }
    }
}

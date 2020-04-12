using NoteTaker.Domain.Dtos;

namespace NoteTaker.Client.Events.NotebookEvents
{
    public class DeleteNotebookCommand
    {
        public DeleteNotebookCommand(NotebookDto dto)
        {
            Dto = dto;
        }

        public NotebookDto Dto { get; }
    }
}

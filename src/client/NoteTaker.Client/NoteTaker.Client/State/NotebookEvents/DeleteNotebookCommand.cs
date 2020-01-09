using NoteTaker.Domain.Dtos;

namespace NoteTaker.Client.State.NotebookEvents
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

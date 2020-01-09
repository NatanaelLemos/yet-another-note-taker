using System.Collections.Generic;
using System.Threading.Tasks;
using NoteTaker.Client.State.NotebookEvents;
using NoteTaker.Domain.Dtos;

namespace NoteTaker.Client.Services
{
    public interface INotebooksAppService
    {
        void StartListeners();

        Task CreateNotebookCommandHandler(CreateNotebookCommand command);

        Task UpdateNotebookCommandHandler(UpdateNotebookCommand command);

        Task DeleteNotebookCommandHandler(DeleteNotebookCommand command);

        Task<NotebookDto> NotebookItemQueryHandler(NotebookQuery query);

        Task<ICollection<NotebookDto>> NotebookListQueryHandler(NotebookQuery query);
    }
}

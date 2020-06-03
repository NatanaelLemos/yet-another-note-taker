using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public interface INotebooksService
    {
        Task<List<NotebookDto>> GetAll();

        Task<NotebookDto> Get(string notebookKey);

        Task<NotebookDto> Create(NotebookDto notebookDto);

        Task<NotebookDto> Update(NotebookDto notebookDto);

        Task Delete(string notebookKey);
    }
}

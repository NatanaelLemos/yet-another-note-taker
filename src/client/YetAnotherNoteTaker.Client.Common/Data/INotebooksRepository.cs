using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Data
{
    public interface INotebooksRepository
    {
        Task<List<NotebookDto>> GetAll(string email, string token);
        Task<NotebookDto> Get(string email, string notebookKey, string token);
        Task<NotebookDto> Create(string email, NotebookDto notebookDto, string token);
        Task<NotebookDto> Update(string email, NotebookDto notebookDto, string token);
        Task Delete(string email, string notebookKey, string token);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Data
{
    public interface INotebooksRepository
    {
        Task<List<NotebookDto>> GetAll(string email);
        Task<NotebookDto> Get(string email, string notebookKey);
        Task<NotebookDto> Create(string email, NotebookDto notebookDto);
        Task<NotebookDto> Update(string email, NotebookDto notebookDto);
        Task Delete(string email, string notebookKey);
    }
}

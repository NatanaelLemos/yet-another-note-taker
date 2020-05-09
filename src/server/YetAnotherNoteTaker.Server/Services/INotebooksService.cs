using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Server.Services
{
    public interface INotebooksService
    {
        Task<List<NotebookDto>> GetAll(string userEmail);

        Task<NotebookDto> Get(string email, string notebookKey);

        Task<NotebookDto> Add(string userEmail, NotebookDto notebook);

        Task<NotebookDto> Update(string userEmail, string notebookKey, NotebookDto notebook);

        Task Delete(string userEmail, string notebookKey);
    }
}

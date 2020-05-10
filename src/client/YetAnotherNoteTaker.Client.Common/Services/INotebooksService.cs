using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public interface INotebooksService
    {
        Task<List<NotebookDto>> GetAll(string email);

        Task<NotebookDto> Create(string email, NotebookDto notebookDto);

        Task<NotebookDto> Update(string email, NotebookDto notebookDto);

        Task Delete(string email, string notebookKey);
    }
}

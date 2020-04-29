using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public interface INotebooksService
    {
        Task<List<NotebookDto>> GetAll(Guid userId);
        Task<NotebookDto> Create(NotebookDto notebookDto);
        Task<NotebookDto> Update(NotebookDto notebookDto);
        Task Delete(Guid id);
    }
}

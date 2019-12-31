using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoteTaker.Domain.Dtos;

namespace NoteTaker.Domain.Services
{
    public interface INotebooksService
    {
        Task<ICollection<NotebookDto>> GetAll();

        Task<NotebookDto> GetById(Guid notebookId);

        Task<NotebookDto> Create(NotebookDto dto);

        Task<NotebookDto> Update(NotebookDto dto);

        Task Delete(NotebookDto notebook);
    }
}

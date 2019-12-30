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

        Task<NotebookDto> CreateOrUpdate(NotebookDto notebookDto);

        Task Delete(NotebookDto notebook);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Domain.Services
{
    public interface INotebooksService
    {
        Task<ICollection<NotebookDto>> GetAll();

        Task<NotebookDto> Create(NewNotebookDto notebook);

        Task<NotebookDto> Update(NotebookDto Notebook);

        Task Delete(NotebookDto notebook);
    }
}

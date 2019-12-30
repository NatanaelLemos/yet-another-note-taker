using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Domain.Data
{
    public interface INotesRepository
    {
        Task<Note> GetById(Guid id);

        Task<List<Note>> GetAll();

        Task<List<Note>> FindByNotebookId(Guid id);

        Task Create(Note entity);

        Task Update(Note entity);

        Task Save();
    }
}

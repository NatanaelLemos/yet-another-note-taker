using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Domain.Data
{
    public interface INotebooksRepository
    {
        Task<Notebook> GetById(Guid id);

        Task<List<Notebook>> GetAll();

        Task Create(Notebook notebook);

        Task Update(Notebook notebook);

        Task Save();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoteTaker.Domain.Data;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Data.Repositories
{
    public class NotebooksRepository : INotebooksRepository
    {
        private readonly NoteTakerContext _ctx;

        public NotebooksRepository(NoteTakerContext ctx)
        {
            _ctx = ctx;
        }

        public Task<Notebook> GetById(Guid id)
        {
            return _ctx.Notebooks
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public Task<List<Notebook>> GetAll()
        {
            return _ctx.Notebooks
                .Where(n => n.Available == true)
                .ToListAsync();
        }

        public async Task Create(Notebook notebook)
        {
            await _ctx.Notebooks.AddAsync(notebook);
        }

        public Task Update(Notebook notebook)
        {
            _ctx.Update(notebook);
            return Task.CompletedTask;
        }

        public Task Save()
        {
            return _ctx.SaveChangesAsync();
        }
    }
}

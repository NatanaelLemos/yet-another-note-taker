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
    public class NotesRepository : INotesRepository
    {
        private readonly NoteTakerContext _ctx;

        public NotesRepository(NoteTakerContext ctx)
        {
            _ctx = ctx;
        }

        public Task<Note> GetById(Guid id)
        {
            return _ctx.Notes.FirstOrDefaultAsync(n => n.Id == id);
        }

        public Task<List<Note>> GetAll()
        {
            return _ctx.Notes.ToListAsync();
        }

        public Task<List<Note>> FindByNotebookId(Guid id)
        {
            return _ctx.Notes
                .Where(n => n.NotebookId == id)
                .ToListAsync();
        }

        public async Task Create(Note entity)
        {
            await _ctx.Notes.AddAsync(entity);
        }

        public Task Update(Note entity)
        {
            _ctx.Update(entity);
            return Task.CompletedTask;
        }

        public Task Save()
        {
            return _ctx.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using YetAnotherNoteTaker.Server.Entities;

namespace YetAnotherNoteTaker.Server.Data
{
    public class NotebooksRepository : INotebooksRepository
    {
        private readonly NoteTakerContext _db;

        public NotebooksRepository(NoteTakerContext db)
        {
            _db = db;
        }

        public async Task<List<Notebook>> GetAll(string userEmail)
        {
            var result = await _db.Notebooks.FindAsync(n => n.UserEmail == userEmail);
            return await result.ToListAsync();
        }

        public async Task<Notebook> Get(string userEmail, string notebookKey)
        {
            var result = await _db.Notebooks.FindAsync(n => n.UserEmail == userEmail && n.Key == notebookKey);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<Notebook> Add(Notebook notebook)
        {
            await _db.Notebooks.InsertOneAsync(notebook);
            return notebook;
        }

        public Task<Notebook> Update(Notebook notebook)
        {
            notebook.Modified = DateTimeOffset.UtcNow;
            return _db.Notebooks.FindOneAndReplaceAsync(n => n.Id == notebook.Id, notebook);
        }

        public Task Delete(Notebook notebook)
        {
            return _db.Notebooks.FindOneAndDeleteAsync(n => n.Id == notebook.Id);
        }
    }
}

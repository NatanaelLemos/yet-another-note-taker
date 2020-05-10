using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using YetAnotherNoteTaker.Server.Entities;

namespace YetAnotherNoteTaker.Server.Data
{
    public class NotesRepository : INotesRepository
    {
        private readonly NoteTakerContext _db;

        public NotesRepository(NoteTakerContext db)
        {
            _db = db;
        }

        public async Task<List<Note>> GetAll(string userEmail)
        {
            var result = await _db.Notes.FindAsync(n => n.Email == userEmail);
            return await result.ToListAsync();
        }

        public async Task<Note> Get(string userEmail, string notebookKey, string noteKey)
        {
            var result = await _db.Notes.FindAsync(n =>
                n.Email == userEmail &&
                n.NotebookKey == notebookKey &&
                n.Key == noteKey);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<List<Note>> GetByNotebookKey(string userEmail, string notebookKey)
        {
            var result = await _db.Notes.FindAsync(n =>
                n.Email == userEmail &&
                n.NotebookKey == notebookKey);

            return await result.ToListAsync();
        }

        public async Task<Note> Add(Note note)
        {
            await _db.Notes.InsertOneAsync(note);
            return note;
        }

        public async Task<Note> Update(Note note)
        {
            note.Modified = DateTimeOffset.UtcNow;
            await _db.Notes.FindOneAndReplaceAsync(n => n.Id == note.Id, note);
            return note;
        }

        public Task Delete(Note note)
        {
            return _db.Notes.FindOneAndDeleteAsync(n => n.Id == note.Id);
        }
    }
}

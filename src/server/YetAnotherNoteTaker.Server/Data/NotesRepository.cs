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

        public Task UpdateEmails(string oldUserEmail, string newUserEmail)
        {
            return _db.Notes.UpdateManyAsync(
                n => n.Email == oldUserEmail,
                Builders<Note>.Update.Set(n => n.Email, newUserEmail));
        }

        public Task UpdateNotebookKeys(string oldNotebookKey, string newNotebookKey)
        {
            return _db.Notes.UpdateManyAsync(
                n => n.NotebookKey == oldNotebookKey,
                Builders<Note>.Update.Set(n => n.NotebookKey, newNotebookKey));
        }

        public Task Delete(Note note)
        {
            return _db.Notes.FindOneAndDeleteAsync(n => n.Id == note.Id);
        }

        public Task DeleteByNotebookKey(string userEmail, string notebookKey)
        {
            return _db.Notes.FindOneAndDeleteAsync(n => n.Email == userEmail && n.NotebookKey == notebookKey);
        }
    }
}

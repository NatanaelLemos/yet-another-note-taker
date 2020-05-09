using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Server.Entities;

namespace YetAnotherNoteTaker.Server.Data
{
    public interface INotesRepository
    {
        Task<List<Note>> GetAll(string userEmail);
        Task<List<Note>> GetByNotebookKey(string userEmail, string notebookKey);
        Task<Note> Get(string userEmail, string notebookKey, string noteKey);
        Task<Note> Add(Note note);
        Task<Note> Update(Note note);
        Task Delete(Note note);
    }
}

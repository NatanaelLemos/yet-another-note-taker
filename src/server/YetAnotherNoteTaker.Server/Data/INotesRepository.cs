using System.Collections.Generic;
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

        Task UpdateEmails(string oldEmail, string newEmail);

        Task UpdateNotebookKeys(string oldNotebookKey, string newNotebookKey);

        Task Delete(Note note);

        Task DeleteByNotebookKey(string userEmail, string notebookKey);
    }
}

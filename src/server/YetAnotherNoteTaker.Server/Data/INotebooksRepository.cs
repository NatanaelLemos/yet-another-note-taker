using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Server.Entities;

namespace YetAnotherNoteTaker.Server.Data
{
    public interface INotebooksRepository
    {
        Task<List<Notebook>> GetAll(string userEmail);

        Task<Notebook> Get(string userEmail, string notebookKey);

        Task<Notebook> Add(Notebook notebook);

        Task<Notebook> Update(Notebook notebook);

        Task UpdateEmails(string oldUserEmail, string newUserEmail);

        Task Delete(Notebook notebook);
    }
}

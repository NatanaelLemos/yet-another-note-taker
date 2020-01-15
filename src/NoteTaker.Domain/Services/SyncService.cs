using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NoteTaker.Domain.Data;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Domain.Services
{
    public class SyncService : ISyncService
    {
        private readonly INotebooksRepository _notebooksRepository;

        public SyncService(INotebooksRepository notebooksRepository)
        {
            _notebooksRepository = notebooksRepository;
        }

        public async Task<string> GetMessages()
        {
            //Remove circular reference
            var notebooks = await _notebooksRepository.GetAll();
            var cleanedNotebooks = new List<Notebook>();

            foreach (var notebook in notebooks)
            {
                var cleanedNotebook = new Notebook
                {
                    Available = notebook.Available,
                    CreatedOn = notebook.CreatedOn,
                    Id = notebook.Id,
                    Name = notebook.Name,
                    UpdatedOn = notebook.UpdatedOn
                };

                foreach (var note in notebook.Notes)
                {
                    cleanedNotebook.Notes.Add(new Note
                    {
                        Available = note.Available,
                        CreatedOn = note.CreatedOn,
                        Id = note.Id,
                        Name = note.Name,
                        NotebookId = note.NotebookId,
                        Text = note.Text,
                        UpdatedOn = note.UpdatedOn
                    });
                }

                cleanedNotebooks.Add(cleanedNotebook);
            }

            return JsonConvert.SerializeObject(cleanedNotebooks);
        }

        public Task UpdateMessages(string message)
        {
            message = message.Trim();

            var notebooks = JsonConvert.DeserializeObject<List<Notebook>>(message);

            return Task.CompletedTask;
        }
    }
}

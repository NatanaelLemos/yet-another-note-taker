using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NoteTaker.Domain.Data;

namespace NoteTaker.Domain.Services
{
    public class SyncService : ISyncService
    {
        private readonly INotebooksRepository _notebooksRepository;
        private readonly INotesRepository _notesRepository;

        public SyncService(INotebooksRepository notebooksRepository, INotesRepository notesRepository)
        {
            _notebooksRepository = notebooksRepository;
            _notesRepository = notesRepository;
        }

        public async Task<string> GetMessages()
        {
            var notebooksTask = _notebooksRepository.GetAll();
            var notesTask = _notesRepository.GetAll();

            await Task.WhenAll(notebooksTask, notesTask);
            var (notebooks, notes) = (await notebooksTask, await notesTask);

            var messages = new StringBuilder();
            messages.Append("\n****notebook****\n");

            foreach (var notebook in notebooks)
            {
                messages.Append(JsonConvert.SerializeObject(notebook));
                messages.Append("\n****notebook****\n");
            }

            messages.Append("****note****\n");

            foreach (var note in notes)
            {
                messages.Append(JsonConvert.SerializeObject(note));
                messages.Append("\n****note****\n");
            }

            return messages.ToString();
        }

        public Task UpdateMessages(string message)
        {
            var lines = message.Split("\n****notebook****\n".ToCharArray());

            return Task.CompletedTask;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _repository;

        public NotesService(INotesRepository repository)
        {
            _repository = repository;
        }

        public Task<List<NoteDto>> GetAll(string email)
        {
            return _repository.GetAll(email);
        }

        public Task<List<NoteDto>> GetByNotebookKey(string email, string notebookKey)
        {
            return _repository.GetByNotebookKey(email, notebookKey);
        }

        public Task<NoteDto> Create(string email, NoteDto noteDto)
        {
            return _repository.Create(email, noteDto);
        }

        public Task<NoteDto> Update(string email, NoteDto noteDto)
        {
            return _repository.Update(email, noteDto);
        }

        public Task Delete(string email, string notebookKey, string noteKey)
        {
            return _repository.Delete(email, notebookKey, noteKey);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLemos.Api.Framework.Exceptions;
using NLemos.Api.Framework.Extensions;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Server.Data;
using YetAnotherNoteTaker.Server.Entities;

namespace YetAnotherNoteTaker.Server.Services
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _repository;

        public NotesService(INotesRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<NoteDto>> GetAll(string userEmail)
        {
            var result = await _repository.GetAll(userEmail);
            return result.Select(ToDto).ToList();
        }

        public async Task<List<NoteDto>> GetByNotebookKey(string userEmail, string notebookKey)
        {
            var result = await _repository.GetByNotebookKey(userEmail, notebookKey);
            return result.Select(ToDto).ToList();
        }

        public async Task<NoteDto> Get(string userEmail, string notebookKey, string noteKey)
        {
            var result = await _repository.Get(userEmail, notebookKey, noteKey);
            return ToDto(result);
        }

        public async Task<NoteDto> Add(string userEmail, string notebookKey, NoteDto dto)
        {
            var key = dto.Name.GenerateKey();
            var current = await _repository.Get(userEmail, notebookKey, key);
            if (current != null)
            {
                throw new InvalidParametersException(nameof(dto.Name), "Name already in use.");
            }

            current = new Note
            {
                Body = dto.Body,
                Email = userEmail,
                Key = key,
                Name = dto.Name,
                NotebookKey = notebookKey
            };

            current = await _repository.Add(current);
            return ToDto(current);
        }

        public async Task<NoteDto> Update(string userEmail, string notebookKey, string noteKey, NoteDto dto)
        {
            var current = await _repository.Get(userEmail, notebookKey, noteKey);
            if (current == null)
            {
                throw new InvalidParametersException(nameof(noteKey), "Note not found.");
            }

            current.Key = dto.Name.GenerateKey();
            current.Name = dto.Name;
            current.Body = dto.Body;

            current = await _repository.Update(current);
            return ToDto(current);
        }

        public async Task Delete(string userEmail, string notebookKey, string noteKey)
        {
            var current = await _repository.Get(userEmail, notebookKey, noteKey);
            if (current == null)
            {
                throw new InvalidParametersException(nameof(noteKey), "Note not found.");
            }

            await _repository.Delete(current);
        }

        private NoteDto ToDto(Note note)
        {
            return new NoteDto
            {
                Key = note.Key,
                Name = note.Name,
                Body = note.Body,
                NotebookKey = note.NotebookKey
            };
        }
    }
}

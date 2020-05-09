using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public class NotesService : INotesService
    {
        private static List<NoteDto> _db = new List<NoteDto>
            {
                new NoteDto { Key = "n1", Name = "n1" }
            };

        public Task<List<NoteDto>> GetAll(string email)
        {
            return Task.FromResult(_db);
        }

        public Task<List<NoteDto>> GetByNotebookKey(string email, string notebookKey)
        {
            return Task.FromResult(
                _db.Where(d => d.NotebookKey == notebookKey).ToList());
        }

        public Task<NoteDto> Create(string email, NoteDto noteDto)
        {
            noteDto.Key = noteDto.Name;
            _db.Add(noteDto);
            return Task.FromResult(noteDto);
        }

        public Task<NoteDto> Update(string email, NoteDto noteDto)
        {
            var dbItem = _db.FirstOrDefault(d => d.Key == noteDto.Key);
            if (dbItem != null)
            {
                _db.Remove(dbItem);
            }

            noteDto.NotebookKey = dbItem.NotebookKey;
            _db.Add(noteDto);
            return Task.FromResult(noteDto);
        }

        public Task Delete(string noteKey)
        {
            var dbItem = _db.FirstOrDefault(d => d.Key == noteKey);
            if (dbItem != null)
            {
                _db.Remove(dbItem);
            }
            return Task.CompletedTask;
        }
    }
}

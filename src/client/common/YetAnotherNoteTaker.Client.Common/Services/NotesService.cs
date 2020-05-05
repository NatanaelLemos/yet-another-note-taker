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
                new NoteDto { Id = Guid.NewGuid(), Name = "n1" }
            };

        public Task<List<NoteDto>> GetAll(Guid userId)
        {
            return Task.FromResult(_db);
        }

        public Task<List<NoteDto>> GetByNotebookId(Guid userId, Guid notebookId)
        {
            return Task.FromResult(
                _db.Where(d => d.NotebookId == notebookId).ToList());
        }

        public Task<NoteDto> Create(Guid userId, NoteDto noteDto)
        {
            noteDto.Id = Guid.NewGuid();
            _db.Add(noteDto);
            return Task.FromResult(noteDto);
        }

        public Task<NoteDto> Update(Guid userId, NoteDto noteDto)
        {
            var dbItem = _db.FirstOrDefault(d => d.Id == noteDto.Id);
            if (dbItem != null)
            {
                _db.Remove(dbItem);
            }

            noteDto.NotebookId = dbItem.NotebookId;
            _db.Add(noteDto);
            return Task.FromResult(noteDto);
        }

        public Task Delete(Guid noteId)
        {
            var dbItem = _db.FirstOrDefault(d => d.Id == noteId);
            if (dbItem != null)
            {
                _db.Remove(dbItem);
            }
            return Task.CompletedTask;
        }
    }
}

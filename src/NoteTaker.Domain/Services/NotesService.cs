using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Domain.Data;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Domain.Services
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _repository;

        public NotesService(INotesRepository repository)
        {
            _repository = repository;
        }

        public async Task<NoteDetailDto> GetById(Guid id)
        {
            var note = await _repository.GetById(id);
            return new NoteDetailDto
            {
                Id = note.Id,
                NotebookId = note.NotebookId,
                Name = note.Name,
                Text = note.Text
            };
        }

        public async Task<ICollection<NoteListItemDto>> GetAll()
        {
            var allNotes = await _repository.GetAll();
            return allNotes.Select(n => new NoteListItemDto
            {
                Id = n.Id,
                Name = n.Name
            }).ToList();
        }

        public async Task<ICollection<NoteListItemDto>> FindByNotebookId(Guid id)
        {
            var notes = await _repository.FindByNotebookId(id);
            return notes.Select(n => new NoteListItemDto
            {
                Id = n.Id,
                Name = n.Name
            }).ToList();
        }

        public async Task<NoteDetailDto> CreateOrUpdate(NoteDetailDto noteDetailDto)
        {
            if (noteDetailDto.Id == Guid.Empty)
            {
                return await Create(noteDetailDto);
            }

            var note = await _repository.GetById(noteDetailDto.Id);

            if (note == null)
            {
                return await Create(noteDetailDto);
            }

            note.Name = noteDetailDto.Name;
            note.Text = noteDetailDto.Text;
            return await Update(note);
        }

        private async Task<NoteDetailDto> Create(NoteDetailDto dto)
        {
            var entity = new Note
            {
                Name = dto.Name,
                Text = dto.Text,
                NotebookId = dto.NotebookId
            };

            await _repository.Create(entity);
            await _repository.Save();

            return new NoteDetailDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Text = entity.Text,
                NotebookId = entity.NotebookId
            };
        }

        private async Task<NoteDetailDto> Update(Note entity)
        {
            await _repository.Update(entity);
            await _repository.Save();

            return new NoteDetailDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Text = entity.Text,
                NotebookId = entity.NotebookId
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<NoteDto> GetById(Guid id)
        {
            var item = await _repository.GetById(id);
            return new NoteDto
            {
                Id = item.Id,
                Name = item.Name,
                NotebookId = item.NotebookId,
                Text = item.Text
            };
        }

        public async Task<ICollection<NoteDto>> GetAll()
        {
            var items = await _repository.GetAll();
            return items.Select(item => new NoteDto
            {
                Id = item.Id,
                Name = item.Name,
                NotebookId = item.NotebookId,
                Text = item.Text
            }).ToList();
        }

        public async Task<ICollection<NoteDto>> FindByNotebookId(Guid id)
        {
            var items = await _repository.FindByNotebookId(id);
            return items.Select(item => new NoteDto
            {
                Id = item.Id,
                Name = item.Name,
                NotebookId = item.NotebookId,
                Text = item.Text
            }).ToList();
        }

        public async Task<NoteDto> Create(NoteDto dto)
        {
            var entity = new Note
            {
                Id = dto.Id,
                Name = dto.Name,
                Text = dto.Text,
                NotebookId = dto.NotebookId
            };

            await _repository.Create(entity);
            await _repository.Save();

            return new NoteDto
            {
                Id = entity.Id,
                Name = dto.Name,
                NotebookId = entity.NotebookId,
                Text = entity.Text
            };
        }

        public async Task<NoteDto> Update(NoteDto dto)
        {
            var entity = await _repository.GetById(dto.Id);
            entity.Name = dto.Name;
            entity.Text = dto.Text;

            await _repository.Update(entity);
            await _repository.Save();

            return new NoteDto
            {
                Id = entity.Id,
                Name = entity.Name,
                NotebookId = entity.NotebookId,
                Text = entity.Text
            };
        }

        public async Task Delete(NoteDto dto)
        {
            var entity = await _repository.GetById(dto.Id);
            entity.Available = false;

            await _repository.Update(entity);
            await _repository.Save();
        }
    }
}

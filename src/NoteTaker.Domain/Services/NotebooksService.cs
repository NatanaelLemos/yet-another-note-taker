using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoteTaker.Domain.Data;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Domain.Services
{
    public class NotebooksService : INotebooksService
    {
        private readonly INotebooksRepository _repository;

        public NotebooksService(INotebooksRepository repository)
        {
            _repository = repository;
        }

        public async Task<NotebookDto> GetById(Guid id)
        {
            var note = await _repository.GetById(id);
            return new NotebookDto
            {
                Id = note.Id,
                Name = note.Name
            };
        }

        public async Task<ICollection<NotebookDto>> GetAll()
        {
            var allNotebooks = await _repository.GetAll();
            return allNotebooks.Select(n => new NotebookDto
            {
                Id = n.Id,
                Name = n.Name
            }).ToList();
        }

        public async Task<NotebookDto> Create(NotebookDto notebook)
        {
            var entity = new Notebook
            {
                Id = notebook.Id,
                Name = notebook.Name
            };

            await _repository.Create(entity);
            await _repository.Save();

            return new NotebookDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public async Task<NotebookDto> Update(NotebookDto notebook)
        {
            var entity = await _repository.GetById(notebook.Id);

            if (entity == null)
            {
                return notebook;
            }

            entity.Name = notebook.Name;
            await _repository.Update(entity);
            await _repository.Save();

            return new NotebookDto
            {
                Id = notebook.Id,
                Name = notebook.Name
            };
        }

        public async Task Delete(NotebookDto notebook)
        {
            var entity = await _repository.GetById(notebook.Id);
            entity.Available = false;
            await _repository.Update(entity);
            await _repository.Save();
        }
    }
}

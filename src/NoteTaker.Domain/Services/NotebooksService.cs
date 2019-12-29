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

        public async Task<ICollection<NotebookDto>> GetAll()
        {
            var allNotebooks = await _repository.GetAll();
            return allNotebooks.Select(n => new NotebookDto
            {
                Id = n.Id,
                Name = n.Name
            }).ToList();
        }

        public async Task<NotebookDto> Create(NewNotebookDto notebook)
        {
            var entity = new Notebook
            {
                Name = notebook?.Name
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
            var dbNotebook = await _repository.GetById(notebook.Id);
            dbNotebook.Name = notebook.Name;

            await _repository.Update(dbNotebook);
            await _repository.Save();

            return notebook;
        }

        public async Task Delete(NotebookDto notebook)
        {
            var dbNotebook = await _repository.GetById(notebook.Id);
            dbNotebook.Available = false;
            await _repository.Update(dbNotebook);
            await _repository.Save();
        }
    }
}

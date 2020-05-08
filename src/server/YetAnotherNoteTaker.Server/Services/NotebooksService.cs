using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLemos.Api.Framework.Exceptions;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Server.Data;
using YetAnotherNoteTaker.Server.Entities;

namespace YetAnotherNoteTaker.Server.Services
{
    public class NotebooksService : INotebooksService
    {
        private readonly INotebooksRepository _repository;

        public NotebooksService(INotebooksRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<NotebookDto>> GetAll(string userEmail)
        {
            var result = await _repository.GetAll(userEmail);
            return result.Select(r => new NotebookDto
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
        }

        public async Task<NotebookDto> Get(string email, Guid id)
        {
            var result = await _repository.Get(email, id);
            return new NotebookDto
            {
                Id = result.Id,
                Name = result.Name
            };
        }

        public async Task<NotebookDto> Add(string userEmail, NotebookDto notebook)
        {
            var current = await _repository.GetByName(userEmail, notebook.Name);
            if (current != null)
            {
                throw new InvalidParametersException(nameof(notebook.Name), "There's already a notebook with this name.");
            }

            current = await _repository.Add(new Notebook
            {
                Name = notebook.Name,
                UserEmail = userEmail
            });

            return new NotebookDto
            {
                Id = current.Id,
                Name = current.Name
            };
        }

        public async Task<NotebookDto> Update(string userEmail, Guid id, NotebookDto notebook)
        {
            var current = await _repository.Get(userEmail, id);
            if (current == null)
            {
                throw new InvalidParametersException(nameof(id), "Notebook not found.");
            }

            current.Name = notebook.Name;
            current = await _repository.Update(current);

            return new NotebookDto
            {
                Id = current.Id,
                Name = current.Name
            };
        }

        public async Task Delete(string userEmail, Guid id)
        {
            var current = await _repository.Get(userEmail, id);
            if (current == null)
            {
                throw new InvalidParametersException(nameof(id), "Notebook not found.");
            }

            await _repository.Delete(current);
        }
    }
}
